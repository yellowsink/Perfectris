using System;
using System.Collections.Generic;
using System.Linq;
using Perfectris.Core.Enums;
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
		
		public static Direction Clockwise(this Direction direction) => direction switch
		{
			Direction.Up    => Direction.Right,
			Direction.Right => Direction.Down,
			Direction.Down  => Direction.Left,
			Direction.Left  => Direction.Up,
			_               => throw new ArgumentOutOfRangeException()
		};

		public static Direction Anticlockwise(this Direction direction) => direction switch
		{
			Direction.Up    => Direction.Left,
			Direction.Right => Direction.Up,
			Direction.Down  => Direction.Right,
			Direction.Left  => Direction.Down,
			_               => throw new ArgumentOutOfRangeException()
		};
		
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

		public static int OpenColumnsFromLeft(this  Tetromino piece) => piece.Grid.OpenColumnsFromLeft();
		public static int OpenColumnsFromRight(this Tetromino piece) => piece.Grid.OpenColumnsFromRight();
	}
}