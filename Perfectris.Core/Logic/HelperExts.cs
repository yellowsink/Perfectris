using System;
using System.Collections.Generic;
using System.Linq;
using Perfectris.Core.Enums;
using Perfectris.Core.StaticValues;
using Perfectris.Core.Types;

namespace Perfectris.Core.Logic
{
	public static class HelperExts
	{
		/// <summary>
		/// Pads an array with a default value to reach the size given, with an offset of where the array given starts
		/// </summary>
		/// <param name="original">The array to pad</param>
		/// <param name="size">How big to pad to. Will use length of original if longer</param>
		/// <param name="value">What to pad with</param>
		/// <param name="offset">How much to offset from the start</param>
		/// <typeparam name="T">The type of the array</typeparam>
		/// <returns>The array padded to the given size</returns>
		public static T[] Pad<T>(this T[] original, int size, T value, int offset = 0)
		{
			var working = new T[size];

			var prepared = Enumerable.Repeat(value, offset).Concat(original).SkipLast(offset).ToArray();

			for (var i = 0; i < Math.Max(size, prepared.Length); i++)
				working[i] = prepared.Length < i ? prepared[i] : value;

			return working;
		}

		/// <summary>
		/// Merges two dictionaries. In collisions dictionary2 wins.
		/// </summary>
		/// <param name="dictionary1">The base dictionary</param>
		/// <param name="dictionary2">The dictionary to merge in</param>
		/// <typeparam name="TKey">Type of the keys</typeparam>
		/// <typeparam name="TValue">Type of the values</typeparam>
		/// <returns>Merged dictionaries as new dictionary</returns>
		public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> dictionary1, Dictionary<TKey, TValue> dictionary2)
		{
			var working = new Dictionary<TKey, TValue>();

			foreach (var (key, value) in dictionary1) working[key] = value;
			foreach (var (key, value) in dictionary2) working[key] = value;

			return working;
		}
		
		/// <summary>
		/// Moves a direction clockwise
		/// </summary>
		public static Direction Clockwise(this Direction direction) => direction switch
		{
			Direction.Up    => Direction.Right,
			Direction.Right => Direction.Down,
			Direction.Down  => Direction.Left,
			Direction.Left  => Direction.Up,
			_               => throw new ArgumentOutOfRangeException()
		};

		/// <summary>
		/// Moves a direction anticlockwise
		/// </summary>
		public static Direction Anticlockwise(this Direction direction) => direction switch
		{
			Direction.Up    => Direction.Left,
			Direction.Right => Direction.Up,
			Direction.Down  => Direction.Right,
			Direction.Left  => Direction.Down,
			_               => throw new ArgumentOutOfRangeException()
		};
		
		/// <summary>
		/// How many open columns are on the left of the grid
		/// </summary>
		public static int OpenColumnsFromLeft(this bool[][] grid)
		{
			var openColumns = 0;

			for (var column = 0; column < grid[0].Length; column++)
			{
				var isOpen = true;
				foreach (var row in grid)
					if (row[column])
						isOpen = false;

				if (isOpen) openColumns++;
				else break;
			}

			return openColumns;
		}
		
		/// <summary>
		/// How many open columns are on the right of the grid
		/// </summary>
		public static int OpenColumnsFromRight(this bool[][] grid)
		{
			var openColumns = 0;

			for (var column = grid[0].Length - 1; column >= 0; column--)
			{
				var isOpen = true;
				foreach (var row in grid)
					if (row[column])
						isOpen = false;

				if (isOpen) openColumns++;
				else break;
			}

			return openColumns;
		}
		
		/// <summary>
		/// How many open rows are on the top of the grid
		/// </summary>
		public static int OpenRowsFromTop(this bool[][] grid)
		{
			var openRows = 0;

			foreach (var row in grid)
			{
				var isOpen = true;
				foreach (var cell in row)
					if (cell)
						isOpen = false;

				if (isOpen) openRows++;
				else break;
			}

			return openRows;
		}
		/// <summary>
		/// How many open rows are on the bottom of the grid
		/// </summary>
		public static int OpenRowsFromBottom(this bool[][] grid)
		{
			var openRows = 0;

			for (var rows = grid[0].Length - 1; rows >= 0; rows--)
			{
				var isOpen = true;
				foreach (var cell in grid[rows])
					if (cell)
						isOpen = false;

				if (isOpen) openRows++;
				else break;
			}

			return openRows;
		}

		public static int OpenColumnsFromLeft(this  Tetromino piece) => piece.Grid.OpenColumnsFromLeft();
		public static int OpenColumnsFromRight(this Tetromino piece) => piece.Grid.OpenColumnsFromRight();
		public static int OpenRowsFromTop(this      Tetromino piece) => piece.Grid.OpenRowsFromTop();
		public static int OpenRowsFromBottom(this   Tetromino piece) => piece.Grid.OpenRowsFromBottom();
		
		/// <summary>
		/// Calculates the spawn point for a tetromino
		/// </summary>
		public static (int, int) GetSpawnPos(this TetrominoType pieceType, int gridSizeX, int gridSizeY)
		{
			var spawnLine = Math.Max(0, gridSizeY - 21); // end up on either row 22 or the top of the grid

			var gridMidpoint = gridSizeX / 2;
			
			var pieceWidth = TetrominoStartingGrids.StartingGrids[pieceType][0].Length;

			var spawnColumn = gridMidpoint - pieceWidth / 2;

			return (spawnColumn, spawnLine);
		}

		/// <summary>
		/// Combines two grids using the given function to pick a cell
		/// </summary>
		public static T[][] CombineGrids<T>(this T[][] grid1, T[][] grid2, Func<T, T, T> combineFunc)
		{
			var workingGrid = new List<T[]>();
			
			for (var y = 0; y < Math.Min(grid1.Length, grid2.Length); y++)
			{
				var workingRow = new List<T>();
				
				var row1 = grid1[y];
				var row2 = grid2[y];

				for (var x = 0; x < Math.Min(row1.Length, row2.Length); x++)
				{
					var cell1 = row1[x];
					var cell2 = row2[x];
					workingRow.Add(combineFunc(cell1, cell2));
				}
				
				workingGrid.Add(workingRow.ToArray());
			}

			return workingGrid.ToArray();
		}
	}
}