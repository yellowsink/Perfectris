using System;
using System.Collections.Generic;
using System.Linq;

namespace Perfectris
{
	public class PieceGenerator
	{
		private Piece[] _bag;
		private Piece[] _nextBag;
		private int     bagIndex;
		private bool    _use7Bag;
		private Random  _random = new();

		public PieceGenerator(bool use7Bag = true)
		{
			_use7Bag = use7Bag;
			_nextBag = GenerateBag(use7Bag);
			AdvanceBags(use7Bag);
		}

		public Piece[] GetQueue()
		{
			if (bagIndex == 7) AdvanceBags(_use7Bag);
			// eg index = 2  /- skip first 2           /- take 2 from the next bag to fill in
			return _bag.Skip(bagIndex).Concat(_nextBag.Take(bagIndex)).ToArray();
		}

		private void AdvanceBags(bool use7Bag)
		{
			_bag     = _nextBag;
			_nextBag = GenerateBag(use7Bag);
		}

		private Piece[] GenerateBag(bool use7Bag)
		{
			var working = new List<Piece>();

			if (use7Bag)
			{
				var allPieces = new List<Piece> { Piece.I, Piece.J, Piece.L, Piece.O, Piece.S, Piece.Z, Piece.T };
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
					working.Add((Piece) _random.Next(6));
			}
			

			return working.ToArray();
		}
	}
}