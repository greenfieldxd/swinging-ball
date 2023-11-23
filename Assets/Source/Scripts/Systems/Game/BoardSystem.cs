using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using Source.Scripts.Components;
using Source.Scripts.Enums;
using Source.Scripts.Main;
using Source.Scripts.Signals;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.Systems.Game
{
    public class BoardSystem : GameSystem
    {
        private BallCell[,] boardBalls;

        public override void OnInit()
        {
            Supyrb.Signals.Get<ColumnTriggerSignal>().AddListener(SetBall);
            CreateBoard();
        }

        private void CreateBoard()
        {
            boardBalls = new BallCell[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    boardBalls[i, j] = new BallCell(BallType.None);
                }
            }
        }

        private void SetBall(Ball ball, int col)
        {
            var count = GetCountInColumn(col);

            if (count >= 3) return;

            boardBalls[count, col].ballType = ball.Type;
            boardBalls[count, col].ball = ball;

            StartCoroutine(FindCombos(0.1f));
        }

        private IEnumerator FindCombos(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            var horizontalCombo = FindHorizontalCombo(boardBalls);
            var verticalCombo = FindVerticalCombo(boardBalls);
            var diagonalCombo = FindDiagonalCombo(boardBalls);

            if (horizontalCombo.Count > 0)
            {
                Remove(horizontalCombo);
            }
            else if (verticalCombo.Count > 0)
            {
                Remove(verticalCombo);
            }
            else if (diagonalCombo.Count > 0)
            {
                Remove(diagonalCombo);
            }
            else if (IsBoardFull(boardBalls)) GameManager.Instance.LoadScene(2, false);
        }

        private List<Tuple<int, int>> FindHorizontalCombo(BallCell[,] board)
        {
            List<Tuple<int, int>> comboList = new List<Tuple<int, int>>();

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col <= board.GetLength(1) - 3; col++)
                {
                    if (AreEqual(board[row, col].ballType, board[row, col + 1].ballType, board[row, col + 2].ballType))
                    {
                        comboList.Add(new Tuple<int, int>(row, col));
                        comboList.Add(new Tuple<int, int>(row, col + 1));
                        comboList.Add(new Tuple<int, int>(row, col + 2));
                    }
                }
            }

            return comboList;
        }

        private List<Tuple<int, int>> FindVerticalCombo(BallCell[,] board)
        {
            List<Tuple<int, int>> comboList = new List<Tuple<int, int>>();

            for (int col = 0; col < board.GetLength(1); col++)
            {
                for (int row = 0; row <= board.GetLength(0) - 3; row++)
                {
                    if (AreEqual(board[row, col].ballType, board[row + 1, col].ballType, board[row + 2, col].ballType))
                    {
                        comboList.Add(new Tuple<int, int>(row, col));
                        comboList.Add(new Tuple<int, int>(row + 1, col));
                        comboList.Add(new Tuple<int, int>(row + 2, col));
                    }
                }
            }

            return comboList;
        }

        private List<Tuple<int, int>> FindDiagonalCombo(BallCell[,] board)
        {
            List<Tuple<int, int>> comboList = new List<Tuple<int, int>>();

            // Check diagonals from top-left to bottom-right
            for (int row = 0; row <= board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col <= board.GetLength(1) - 3; col++)
                {
                    if (AreEqual(board[row, col].ballType, board[row + 1, col + 1].ballType, board[row + 2, col + 2].ballType))
                    {
                        comboList.Add(new Tuple<int, int>(row, col));
                        comboList.Add(new Tuple<int, int>(row + 1, col + 1));
                        comboList.Add(new Tuple<int, int>(row + 2, col + 2));
                        return comboList;
                    }
                }
            }

            // Check diagonals from top-right to bottom-left
            for (int row = 0; row <= board.GetLength(0) - 3; row++)
            {
                for (int col = board.GetLength(1) - 1; col >= 2; col--)
                {
                    if (AreEqual(board[row, col].ballType, board[row + 1, col - 1].ballType, board[row + 2, col - 2].ballType))
                    {
                        comboList.Add(new Tuple<int, int>(row, col));
                        comboList.Add(new Tuple<int, int>(row + 1, col - 1));
                        comboList.Add(new Tuple<int, int>(row + 2, col - 2));
                        return comboList;
                    }
                }
            }

            return comboList;
        }

        private bool AreEqual(params BallType[] values)
        {
            BallType first = values[0];
            foreach (var value in values)
            {
                if (value != first || value == BallType.None)
                    return false;
            }

            return true;
        }
       
        static void Shift(BallCell[,] board)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            for (int col = 0; col < cols; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    if (board[row, col].ballType != BallType.None)
                    {
                        int targetRow = row;

                        while (targetRow - 1 >= 0 && board[targetRow - 1, col].ballType == BallType.None)
                        {
                            board[targetRow - 1, col].ballType = board[targetRow, col].ballType;
                            board[targetRow - 1, col].ball = board[targetRow, col].ball;
                            board[targetRow, col].ballType = BallType.None;
                            board[targetRow, col].ball = null;
                            targetRow--;
                        }
                    }
                }
            }
        }
    

        private void Remove(List<Tuple<int, int>> comboBalls)
        {
            var type = boardBalls[comboBalls[0].Item1, comboBalls[0].Item2].ballType;
            Supyrb.Signals.Get<UpdateScoreSignal>().Dispatch(type);
            
            foreach (var comboBall in comboBalls)
            {
                if (boardBalls[comboBall.Item1, comboBall.Item2].ball != null)
                {
                    var ballCell = boardBalls[comboBall.Item1, comboBall.Item2];
                    var effect = LeanPool.Spawn(ballCell.ball.Effect, ballCell.ball.transform.position, Quaternion.identity);
                    LeanPool.Despawn(effect, 1f);
                    
                    Destroy(ballCell.ball.gameObject);
                    ballCell.ballType = BallType.None;
                    ballCell.ball = null;
                }
            }
            
            Shift(boardBalls);
            StartCoroutine(FindCombos(0.1f));
        }

        private int GetCountInColumn(int column)
        {
            int rows = boardBalls.GetLength(0);
            int count = 0;

            for (int row = 0; row < rows; row++)
            {
                if (boardBalls[row, column].ballType != BallType.None)
                {
                    count++;
                }
            }

            return count;
        }

        private bool IsBoardFull(BallCell[,] board)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            for (int col = 0; col < cols; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    if (board[row, col].ballType == BallType.None)
                        return false;
                }
            }

            return true;
        }
    }
}