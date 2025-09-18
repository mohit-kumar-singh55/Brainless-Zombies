using System.Collections.Generic;

public interface IBrainSpawner
{
    Queue<BrainController> Brains { get; }

    void AddBrain(BrainController brain);

    void DeleteBrain(BrainController brain);
}
