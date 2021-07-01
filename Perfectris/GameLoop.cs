using System;
using System.Threading;
using System.Threading.Tasks;

namespace Perfectris
{
	public class GameLoop<TState> where TState : new()
	{
		/// <summary>
		/// Time between each timer tick
		/// </summary>
		private decimal _tickTime;
		/// <summary>
		/// How behind we are on time
		/// </summary>
		private decimal _timeDebt;
		/// <summary>
		/// Which tick the timer is on - starts at 0
		/// </summary>
		public int CurrentTick { get; private set; }

		private Task? _task;

		public TState GameState = new();

		public GameLoop(Action<GameLoop<TState>> render, Func<GameLoop<TState>, bool> isRenderNecessary, bool startNow = false, int tickRate = 100)
		{
			_tickTime = decimal.One / tickRate;

			if (!startNow) return;
			var creationOptions = Task.Factory.CreationOptions | TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness;
			_task = Task.Factory.StartNew(() =>
										  {
											  RunLoop(isRenderNecessary, render);
										  }, creationOptions);
		}

		private void RunLoop(Func<GameLoop<TState>, bool> isRenderNecessary, Action<GameLoop<TState>> render)
		{
			CurrentTick++;
			// start timing for tick
			var startTime = DateTime.Now;
			if (isRenderNecessary(this)) render(this);
			var endTime    = DateTime.Now;
			var renderTime = (decimal) startTime.Subtract(endTime).TotalSeconds;
			_timeDebt = Math.Min(0, _tickTime - renderTime);
			var timeToWait = (long) Math.Min(0, renderTime - _tickTime + _timeDebt) * 10_000_000; // 10,000,000 converts seconds to ticks
			Thread.Sleep(new TimeSpan(timeToWait));
		}
	}
}