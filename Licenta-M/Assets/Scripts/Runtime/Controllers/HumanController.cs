using UnityEngine;

namespace MF
{
	[RequireComponent(typeof(HumanConfigurator))]
	public class HumanController : CustomActorController<HumanModel, HumanView, Movement, NervousSystem>
	{
		private const float DECREASE_TIME = 1f;
		private float lastTime;

		public override void Start()
		{
			lastTime = Time.time;
		}

		private void Update()
		{
			if (Time.time - lastTime > DECREASE_TIME)
			{
				lastTime = Time.time;

				Model.Health.Value = Model.Health.Value < 1 ? 0 : DecreseHealth(Model.Health.Value);
				Model.Money.Value = Model.Money.Value < 1 ? 0 : DecreseMoney(Model.Money.Value);
				Model.Energy.Value = Model.Energy.Value < 1 ? 0 : DecreseEnergy(Model.Energy.Value);
				Model.Hunger.Value = Model.Hunger.Value < 1 ? 0 : DecreseHunger(Model.Hunger.Value);
			}
		}

		public float DecreseEnergy(float modelEnergyValue)
		{
			var decreseEnergyValue = 1f;

			if (Model.CurrentActivity == Activities.IsWorking)
			{
				decreseEnergyValue *= 3f;      // because human spends more time for doing work
			}

			if (Model.CurrentActivity == Activities.IsEating)
			{
				decreseEnergyValue *= 0.5f;      // because human eats
			}

			if (Model.CurrentActivity == Activities.IsSlepping)
			{
				decreseEnergyValue *= 0f;      // because human does`t lose energy when he sleeps
			}

			if (Model.CurrentActivity == Activities.IsMoving)
			{
				decreseEnergyValue *= 2f;      // because human moves
			}

			modelEnergyValue = modelEnergyValue - decreseEnergyValue;
			return modelEnergyValue;
		}

		public float DecreseHunger(float modelHungerValue)
		{
			var decreseHungerValue = 1f;

			if (Model.CurrentActivity == Activities.IsWorking)
			{
				decreseHungerValue *= 4f;      // because human spends more time for doing something and lose more calories
			}

			if (Model.CurrentActivity == Activities.IsEating)
			{
				decreseHungerValue *= 0f;      // because human eats
			}

			if (Model.CurrentActivity == Activities.IsSlepping)
			{
				decreseHungerValue *= 0.5f;      // because human sleeps and he consumes some calories
			}

			if (Model.CurrentActivity == Activities.IsMoving)
			{
				decreseHungerValue *= 1.5f;      // because human moves  he consume calories
			}

			modelHungerValue = modelHungerValue - decreseHungerValue;
			return modelHungerValue;
		}

		public float DecreseMoney(float modelMoneyValue)
		{
			var decreseMoneyValue = 1f;

			if (Model.CurrentActivity == Activities.IsWorking)
			{
				decreseMoneyValue *= 0f;      // because human earns money, doesn`t lose
			}

			if (Model.CurrentActivity == Activities.IsEating)
			{
				decreseMoneyValue *= 5f;      // because human eats
			}

			if (Model.CurrentActivity == Activities.IsSlepping)
			{
				decreseMoneyValue *= 0f;      // because human sleeps
			}

			if (Model.CurrentActivity == Activities.IsMoving)
			{
				decreseMoneyValue *= 0.5f;      // because human moves 
			}

			modelMoneyValue = modelMoneyValue - decreseMoneyValue;
			return modelMoneyValue;
		}

		public float DecreseHealth(float modelHealthValue)
		{
			var decreseHealthValue = 0f;

			modelHealthValue = modelHealthValue - decreseHealthValue;
			return modelHealthValue;
		}
	}
}
