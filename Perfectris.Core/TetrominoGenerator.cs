using System;
using System.Collections.Generic;
using System.Linq;
using Perfectris.Core.Enums;

namespace Perfectris.Core
{
	public static class TetrominoGenerator
	{
		private static TetrominoType[] _bag;
		private static TetrominoType[] _nextBag;
		private static int             _bagIndex;
		private static bool            _use7Bag;
		
		/// <summary>
		/// Whether to use 7bag. Setting resets bags.
		/// </summary>
		public static bool Use7Bag
		{
			get => _use7Bag;
			set
			{
				_use7Bag = value;
				_nextBag = GenerateBag(value);
				AdvanceBags(value);
			}
		}
		
		private static Random          _random = new();

		static TetrominoGenerator() => Use7Bag = true;

		public static void AdvanceQueue() => _bagIndex++;

		public static TetrominoType GetNextAndAdvance()
		{
			var next = GetQueue()[0];
			AdvanceQueue();
			return next;
		}

		public static TetrominoType[] GetQueue()
		{
			while (_bagIndex >= 7) AdvanceBags(Use7Bag);
			// eg index = 2  /- skip first 2           /- take 2 from the next bag to fill in
			return _bag.Skip(_bagIndex).Concat(_nextBag.Take(_bagIndex)).ToArray();
		}

		private static void AdvanceBags(bool use7Bag)
		{
			_bag      =  _nextBag;
			_nextBag  =  GenerateBag(use7Bag);
			_bagIndex -= 7;
		}

		private static TetrominoType[] GenerateBag(bool use7Bag)
		{
			var working = new List<TetrominoType>();

			if (use7Bag)
			{
				var allPieces = new List<TetrominoType> { TetrominoType.I, TetrominoType.J, TetrominoType.L, TetrominoType.O, TetrominoType.S, TetrominoType.Z, TetrominoType.T };
				for (var i = 0; i < 7; i++)
				{
					var randomIndex = _random.Next(allPieces.Count - 1);
					working.Add(allPieces[randomIndex]);
					allPieces.RemoveAt(randomIndex);
				}
			}
			else
			{
				for (var i = 0; i < 7; i++)
					working.Add((TetrominoType) _random.Next(6));
			}
			

			return working.ToArray();
		}
	}
}