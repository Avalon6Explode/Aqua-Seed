using UnityEngine;

public abstract class Skill : MonoBehaviour {

	protected enum State {
		READY,
		ACTIVE,
		COOLDOWN
	}


	[SerializeField]
	protected float cooldownRate;

	[SerializeField]
	[Range(0, 100)]
	protected int staminaCost;


	protected State state;


	public bool IsReady { get { return state == State.READY; } }
	public bool IsActive { get { return state == State.ACTIVE; } }
	public bool IsCoolDown { get { return state == State.COOLDOWN; } }

	public float CooldownRate { get { return cooldownRate; } }
	public int StaminaCost { get { return staminaCost; } }


	public Skill() {
		staminaCost = 0;
		cooldownRate = 0.4f;
		state = State.READY;
	}

	public abstract void Use();
}
