using System;
using System.Collections.Generic;
using System.Linq;

namespace Perfectris.Core
{
	public class TetrominoGenerator
	{
		private TetrominoType[] _bag;
		private TetrominoType[] _nextBag;
		private int             _bagIndex;
		private bool            _use7Bag;
		private Random          _random = new();

		public TetrominoGenerator(bool use7Bag = true)
		{
			_use7Bag = use7Bag;
			_nextBag = GenerateBag(use7Bag);
			AdvanceBags(use7Bag);
		}

		public void AdvanceQueue() => _bagIndex++;

		public TetrominoType[] GetQueue()
		{
			while (_bagIndex >= 7) AdvanceBags(_use7Bag);
			// eg index = 2  /- skip first 2           /- take 2 from the next bag to fill in
			return _bag.Skip(_bagIndex).Concat(_nextBag.Take(_bagIndex)).ToArray();
		}

		private void AdvanceBags(bool use7Bag)
		{
			_bag      =  _nextBag;
			_nextBag  =  GenerateBag(use7Bag);
			_bagIndex -= 7;
		}

		private TetrominoType[] GenerateBag(bool use7Bag)
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