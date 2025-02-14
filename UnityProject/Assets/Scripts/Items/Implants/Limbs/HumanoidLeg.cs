using Player.Movement;
using UnityEngine;

namespace HealthV2.Limbs
{
	public class HumanoidLeg : Limb, IMovementEffect
	{
		[SerializeField]
		[Tooltip("The walking speed that will be used when attached as a leg.\n" +
		         "Additive with any additional legs.\n" +
		         "Multiplied by leg efficiency.")]
		private float walkingSpeed = 1.5f;
		public float WalkingSpeed => walkingSpeed;

		[SerializeField]
		[Tooltip("The running speed that will be used when attached as a leg.\n" +
		         "Additive with any additional legs.\n" +
		         "Multiplied by leg efficiency.")]
		private float runningSpeed = 3f;
		public float RunningSpeed => runningSpeed;

		[SerializeField]
		[Tooltip("A generalized number representing how efficient a leg this limb is.\n" +
		         "1 is a human leg.")]
		private float legEfficiency = 1f;
		public float LegEfficiency => legEfficiency;

		public float RunningSpeedModifier => runningSpeed * legEfficiency * bodyPart.TotalModified;

		public float WalkingSpeedModifier => walkingSpeed * legEfficiency * bodyPart.TotalModified;
		public float CrawlingSpeedModifier { get; }

		public override void AddedToBody(LivingHealthMasterBase livingHealth)
		{
			base.AddedToBody(livingHealth);
			playerHealth.OrNull()?.PlayerMove.AddLeg(this);
			playerHealth.OrNull()?.PlayerMove.AddModifier(this);
		}

		public override void RemovedFromBody(LivingHealthMasterBase livingHealth)
		{
			base.RemovedFromBody(livingHealth);
			playerHealth.OrNull()?.PlayerMove.RemoveLeg(this);
		}

		public void SetNewSpeeds(float newRunningSpeed, float newWalkingSpeed)
		{
			runningSpeed = newRunningSpeed;
			walkingSpeed = newWalkingSpeed;
			ModifierChanged();
		}
	}
}