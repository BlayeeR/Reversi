﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class TileManager
    {
        List<List<Tile>> tiles;
        private Vector2 _gameBoardPosition, _gameBoardDimensions;
        private int boardSize = 8;
        List<Movement> movements;
        private event EventHandler OnSideChange, OnMovePerformed;
        public event EventHandler OnGameEnded;
        private Button2D scoreText;
        private bool _currentSide, _singleplayer = false;
        private double aiMovementDelay = 0;
        public int previousMovements = 0;
        public Score[] score = new Score[2];
        private bool CurrentSide { set { _currentSide = value;//false player1, true ai/player2
                OnSideChange(this, null);
            } get { return _currentSide; } }
        public TileManager(Vector2 gameBoardPosition, Vector2 gameBoardDimensions, bool singleplayer)
        {
            _singleplayer = singleplayer;
            _gameBoardPosition = gameBoardPosition;
            _gameBoardDimensions = gameBoardDimensions;
            tiles = new List<List<Tile>>();
            Vector2 tileSize = new Vector2(_gameBoardDimensions.X / boardSize * 0.96f);
            for(int i =0; i < boardSize; i++)
            {
                List<Tile> tempTiles = new List<Tile>();
                for (int j = 0; j < boardSize; j++)
                {
                    Tile tile = new Tile(true, false, new Vector2(_gameBoardPosition.X + 0.02f * _gameBoardDimensions.X + j * tileSize.X + tileSize.X / 2, _gameBoardPosition.Y + 0.02f * _gameBoardDimensions.Y + i * tileSize.Y + tileSize.Y / 2), tileSize);
                    tile.OnTilePressed += Tile_OnTilePressed;
                    tile.OnMouseOut += Tile_OnMouseOut;
                    tile.OnMouseOver += Tile_OnMouseOver;
                    tempTiles.Add(tile);
                    
                }
                tiles.Add(tempTiles);
            }
            tiles[(int)boardSize / 2 - 1][(int)boardSize / 2 - 1].Visible = true;
            tiles[(int)boardSize / 2][(int)boardSize / 2].Visible = true;
            tiles[(int)boardSize / 2-1][(int)boardSize / 2].Visible = true;
            tiles[(int)boardSize / 2 - 1][(int)boardSize / 2].ChangeSide();
            tiles[(int)boardSize / 2][(int)boardSize / 2-1].Visible = true;
            tiles[(int)boardSize / 2][(int)boardSize / 2 - 1].ChangeSide();
            score[0] = new Score() { PlayerName = "", Value = 0 };
            score[1] = new Score() { PlayerName = "", Value = 0 };
            scoreText = new Button2D("TitleScreen/Button", new Vector2(225, 850), $"", "TitleScreen/CreditsFont");
            scoreText.UnselectedFontColor = Color.Black;
            if (_singleplayer)
                scoreText._text = $"Player score: {score[0].Value}\nPlayer disks: {CountTiles(false)}\nEnemy disks: {CountTiles(true)}";
            else
                scoreText._text = $"Black score: {score[0].Value}\nWhite score: {score[1].Value}\nBlack disks: {CountTiles(false)}\nWhite disks: {CountTiles(true)}";
            OnSideChange += TileManager_OnSideChange;
            OnMovePerformed += TileManager_OnMovePerformed;
            movements = CalculatePossibleMovements(false);
            
        }

        private void TileManager_OnMovePerformed(object sender, EventArgs e)
        {
            Movement movement = (sender as Movement);
            if (!movement.Side )
            {
                score[0].Value += movement.Score();
            }
            else if (!_singleplayer)
            {
                score[1].Value += movement.Score();
            }
            if (_singleplayer)
                scoreText._text = $"Player score: {score[0].Value}\nPlayer disks: {CountTiles(false)}\nEnemy disks: {CountTiles(true)}";
            else
                scoreText._text = $"Black score: {score[0].Value}\nWhite score: {score[1].Value}\nBlack disks: {CountTiles(false)}\nWhite disks: {CountTiles(true)}";
        }

        private void TileManager_OnSideChange(object sender, EventArgs e)
        {
            previousMovements = movements.Count;
            movements = CalculatePossibleMovements(CurrentSide);
            if (movements.Count == 0)
            {
                if (previousMovements == 0)
                {
                    GameEndedEventArgs gameEndedEventArgs;
                    if (CountTiles(false) > CountTiles(true))//player win
                        gameEndedEventArgs = new GameEndedEventArgs(-1, score);
                    else if (CountTiles(false) == CountTiles(true))
                        gameEndedEventArgs = new GameEndedEventArgs(0, score);
                    else
                        gameEndedEventArgs = new GameEndedEventArgs(1, score);
                    OnGameEnded(this, gameEndedEventArgs);
                }
                else
                    CurrentSide = !CurrentSide;
            }
        }

        private void Tile_OnMouseOver(object sender, EventArgs e)
        {
            if (!_singleplayer || (_singleplayer && !CurrentSide))
            {
                foreach (Movement movement in movements)
                {
                    if ((sender as Tile).Equals(movement.DestinationTile))
                    {
                        movement.DestinationTile.DrawingColor = Color.LightGray;
                        movement.DestinationTile.Side = movement.Side;
                        movement.DestinationTile.Visible = true;
                        movement.SourceTile.DrawingColor = Color.LightGray;
                        foreach (Tile tile in movement.TakenTiles)
                            tile.DrawingColor = Color.LightGray;
                    }
                }
            }
        }

        private void Tile_OnMouseOut(object sender, EventArgs e)
        {
            if (!_singleplayer || (_singleplayer && !CurrentSide))
            {
                foreach (Movement movement in movements)
                {
                    if ((sender as Tile).Equals(movement.DestinationTile))
                    {
                        movement.DestinationTile.DrawingColor = Color.DarkGray;
                        movement.SourceTile.DrawingColor = Color.White;
                        movement.DestinationTile.Visible = false;
                        foreach (Tile tile in movement.TakenTiles)
                            tile.DrawingColor = Color.White;
                    }
                }
            }
        }

        private void Tile_OnTilePressed(object sender, EventArgs e)
        {
            if (!_singleplayer||(_singleplayer&&!CurrentSide))
            {
                bool performed = false;
                for (int i = 0; i < movements.Count; i++)
                {
                    if ((sender as Tile).Equals(movements[i].DestinationTile))
                    {
                        movements[i].Perform();
                        OnMovePerformed(movements[i], null);
                        performed = true;
                    }
                }
                if (performed)
                    CurrentSide = !CurrentSide;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            scoreText.Draw(spriteBatch);
            foreach (List<Tile> tilei in tiles)
                foreach (Tile tile in tilei)
                    tile.Draw(spriteBatch);
        }

        public int CountTiles(bool side)
        {
            int i = 0;
            foreach (List<Tile> tilelist in tiles)
                foreach (Tile tile in tilelist.Where(x => x.Side == side && x.Visible))
                    i++;
            return i;
        }

        public void Update(GameTime gameTime)
        {
            scoreText.Update(gameTime);
            if(CurrentSide && _singleplayer)
            {

                aiMovementDelay += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (aiMovementDelay >= 1)
                {
                    Movement movement = CalculatePossibleMovements(CurrentSide).OrderBy(o => o.TakenTiles.Count).Last();
                    movement.Perform();
                    OnMovePerformed(movement, null);
                    CurrentSide = !CurrentSide;
                    aiMovementDelay = 0;
                }
            }
            foreach (List<Tile> tilei in tiles)
                foreach (Tile tile in tilei)
                    tile.Update(gameTime);
        }
        public List<Movement> CalculatePossibleMovements(bool side)
        {
            List<Movement> movements = new List<Movement>();
            foreach (List<Tile> tilelist in tiles)
                foreach (Tile tile in tilelist)
                    tile.DrawingColor = Color.White;
            foreach (List<Tile> tilelist in tiles)
            {
                foreach(Tile tile in tilelist.Where(x => x.Side == side && x.Visible))
                {
                    int xi=0, yi=0;
                    for(int i =0; i< 8;i++)
                    {
                        int x = tiles.IndexOf(tilelist), y = tilelist.IndexOf(tile);
                        List<Tile> takenTiles = new List<Tile>();
                        takenTiles.Add(tile);
                        switch (i)
                        {
                            case 0: { xi = 0;yi = 1;break; }
                            case 1: { xi = 0; yi = -1; break; }
                            case 4: { xi = 1; yi = 1; break; }
                            case 5: { xi = 1; yi = -1; break; }
                            case 6: { xi = 1; yi = 0; break; }
                            case 2: { xi = -1; yi = -1; break; }
                            case 3: { xi = -1; yi = 1; break; }
                            case 7: { xi = -1; yi = 0; break; }
                        }
                        while (x >= 0 && x < boardSize && y >= 0 && y < boardSize)
                        {
                            if (xi == 1)
                                x++;
                            else if (xi == -1)
                                x--;
                            if (yi == 1)
                                y++;
                            else if (yi == -1)
                                y--;
                            if (x < 0 || x >= boardSize || y < 0 || y >= boardSize)
                                break;
                            if (!tiles[x][y].Visible)
                            {
                                if (!tile.Equals(takenTiles.Last()))
                                {
                                    //new Movement(side, takenTiles.First(), takenTiles.Last(), takenTiles.Where(a => !a.Equals(takenTiles.First())).Where(b => !b.Equals(takenTiles.Last())).ToList());
                                    movements.Add(new Movement(side, takenTiles.First(), tiles[x][y], takenTiles.Where(a => !a.Equals(takenTiles.First())).ToList(), _singleplayer?(CurrentSide?false:true):true));
                                }
                                break;
                            }
                            else if (tiles[x][y].Side == tile.Side)
                                break;
                            takenTiles.Add(tiles[x][y]);

                        }
                        
                    }
                }
            }
            return movements;
        }
    }
}
