using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ChessOut.MapSystem
{
    //Handles the game map
    //Both the Tile map and the Data map
    public class Map : IMyDrawable
    {
        private readonly int _size;

        //A map of the background tiles
        private ActorElement[,] _tileMap;

        //The actual data of the map (enemies, player, objects, etc)
        private MapElement[,] _dataMap;
        public Map(MapData mapData) 
        { 
            _size = mapData.Size * GameData.SectionSize;

            //Procedurally generating a map of sections
            SectionsMap sectionsMap = new SectionsMap(mapData.Size,mapData.SectionsToGenerate);
            //Creating the background tile map from the sections map
            GenerateTileMap(sectionsMap);
            //Creating the actual matrix the data sits on during the level
            GenerateDataMap(sectionsMap, mapData);
        }

        //Return the element at the given position from the data map
        public MapElement ElementAt(Point position)
        {
            if (position.Y < 0 || position.X < 0 || position.Y >= _size || position.X >= _size) return null;

            return _dataMap[position.Y, position.X];
        }

        //Whoever calls the function is in charge of making sure it can override whoever it is walking into and that it is within map bounds
        public void MoveElementAtInDirection(Point position, Direction direction, MapElement element)
        {
            if (ElementAt(position) != element) return;

            Point newPosition = new Point(position).MovePointInDirection(direction);
            _dataMap[newPosition.Y, newPosition.X] = _dataMap[position.Y, position.X];
            _dataMap[position.Y, position.X] = EmptyElement.InnerInstance;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Camera camera = RunManager.CurrentLevel.Camera;

            for (int i = camera.Origin.Y; i < camera.Origin.Y + camera.Height; i++)
            {
                for (int j = camera.Origin.X; j < camera.Origin.X + camera.Width; j++)
                {
                    //The camera can be outside of the matrix borders, so we make sure not to draw there
                    if (i < 0 || j < 0 || i >= _size || j >= _size) continue;

                    //Makes sure to not draw non existing tiles, which also means no data can be there to draw as well
                    if (_tileMap[i, j] == null) continue;

                    //Draws the background tile
                    _tileMap[i, j].Draw(gameTime, spriteBatch);

                    //Draws the element on the tile if there is one there
                    if (_dataMap[i, j] is IMyDrawable drawable) drawable.Draw(gameTime, spriteBatch);

                }
            }
        }
        private void GenerateDataMap(SectionsMap sectionsMap,MapData mapData)
        {
            //Give sections their types (e.g. makes sure all enemy and chest types are allocated)
            sectionsMap.AllocateSectionsType(mapData.ChestSectionsToGenerate, SectionType.Chest);
            sectionsMap.AllocateSectionsType(mapData.EnemySectionsToGenerate, SectionType.Enemy);

            //Generate section layouts (e.g. each section places their elements into their layout)
            sectionsMap.GenerateSectionsLayout();

            //Move the layouts into the dataMap one to one
            _dataMap = new MapElement[_size, _size];
            int sectionSize = GameData.SectionSize;
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _dataMap[i, j] = sectionsMap.Sections[i / sectionSize, j / sectionSize].SectionLayout[i % sectionSize, j % sectionSize];
                }
            }

            //Generate World Borders (e.g. every outer element that touches a non outer or world border becomes a world border)
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (IsWorldBorder(i, j))
                    {
                        _dataMap[i, j] = new WorldBorder();
                    }
                }
            }        
        }

        //Check if the Empty Outer Element is a world border if any neighboring tile is an Empty Inner Element
        private bool IsWorldBorder(int i,int j)
        {
            Point mapPosition = new Point(j, i);

            if(ElementAt(mapPosition) != EmptyElement.OuterInstance) return false;

            Direction[] directionsArray = (Direction[])Enum.GetValues(typeof(Direction));
            List<Direction> directions = new List<Direction>(directionsArray);

            Point elementInDirectionPosition;
            MapElement elementInDirection;
            foreach (Direction direction in directions)
            {
                elementInDirectionPosition = new Point(mapPosition).MovePointInDirection(direction);
                elementInDirection = ElementAt(elementInDirectionPosition);

                if (elementInDirection == null) continue;

                if (elementInDirection != EmptyElement.OuterInstance && !(elementInDirection is WorldBorder))
                {
                    return true;
                }
            }
            return false;
        }
        
        //Creates the tile map from the sections map, by giving every position with an Empty Inner Element a randomized tile
        private void GenerateTileMap(SectionsMap sectionsMap)
        {
            _tileMap = new ActorElement[_size, _size];
            int sectionSize = GameData.SectionSize;
            
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (sectionsMap.Sections[i / sectionSize, j / sectionSize].SectionLayout[i % sectionSize, j % sectionSize] == EmptyElement.OuterInstance)
                    {
                        _tileMap[i, j] = null;
                    }
                    else
                    {
                        GenerateTile(i, j);
                    }    
                }
            }
        }

        //Returns a tile that is either dark or light variant depending on the position
        private void GenerateTile(int i,int j)
        {
            Point mapPosition = new Point(j, i);
            Texture2D tileSprite;
            if ((i + j) % 2 == 0)
            {
                tileSprite = AssetsManager.RandomTileByVariant(TileVariant.Dark);
            }
            else
            {
                tileSprite = AssetsManager.RandomTileByVariant(TileVariant.Light);

            }
            _tileMap[i, j] = new ActorElement(tileSprite, mapPosition);
        }

        
    }
}
