using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileTesting
{
    public class PathFindTileMob
    {
        public Vector2 Position { get; private set; }
        private Texture2D texture;

        private PathFindingCode.PathFinderTile pathFinder;

        private List<Vector2> nodes;

        private LevelManager levelManager;

        private Vector2 direction, destination;

        private float speed = 100;
        private bool moving = false;

        public PathFindTileMob(LevelManager levelManager, Vector2 startPosition, Vector2 endPosition)
        {
            Position = startPosition;
            texture = TextureManager.CatTex;

            this.levelManager = levelManager;

            pathFinder = new PathFindingCode.PathFinderTile(startPosition, endPosition, levelManager.Tiles);
            nodes = pathFinder.FindPath();
        }

        public void Update(GameTime gameTime, Player player)
        {
            //detta funkar inte då update sker för ofta (kommentar som skrevs innan if-satsen)
            if (player.oldPosition != player.Position)
            {
                pathFinder = new PathFindingCode.PathFinderTile(Position, player.hitbox.Center.ToVector2(), levelManager.Tiles);
                nodes = pathFinder.FindPath();
            }

            Moving(gameTime);
        }

        private void Moving(GameTime gameTime)
        {
            if (!moving)
            {
                int x = 0;
                int y = 0;
                if (nodes.Count != 0)
                {
                    if (Position == nodes.First())
                    {
                        nodes.RemoveAt(0);
                    }

                    if (Position.X < nodes.First().X)
                    {
                        x = 1;
                    }
                    if (Position.X > nodes.First().X)
                    {
                        x = -1;
                    }
                    if (Position.Y < nodes.First().Y)
                    {
                        y = 1;
                    }
                    if (Position.Y > nodes.First().Y)
                    {
                        y = -1;
                    }

                    Vector2 nextNode = new Vector2(x, y);

                    ChangeDirection(nextNode);
                }
            }

            else
            {
                Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Vector2.Distance(Position, destination) < 1)
                {
                    Position = destination;
                    moving = false;
                    if (nodes.Count != 0)
                    {
                        nodes.RemoveAt(0);
                    }
                }
            }

        }

        private void ChangeDirection(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = Position + direction * ConstantValues.TILE_WIDTH;

            Tile tile = levelManager.GetTileAtPosition(direction);
            if (tile.IsWall)
            {
                destination = newDestination;
                moving = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y,
                ConstantValues.TILE_WIDTH, ConstantValues.TILE_HEIGHT), new Rectangle(0, 0,
                ConstantValues.TILE_FRAME_SIZE, ConstantValues.TILE_FRAME_SIZE), Color.White);
        }
    }
}
