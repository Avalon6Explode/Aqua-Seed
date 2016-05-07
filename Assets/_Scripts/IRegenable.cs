using System.Collections;

public interface IRegenable {

	float RegenRate { get; }
	float RegenDelay { get; }
	int RegenPoint { get; }
	bool IsRegenning { get; }

	void Regen();
}