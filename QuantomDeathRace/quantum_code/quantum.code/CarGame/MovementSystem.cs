using Photon.Deterministic;
using Quantum;

namespace Quantum.CarGame;

public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter> 
{
    public struct Filter
    {
        public EntityRef Entity;
        public VehicleController3D* VehicleController3D;
        public Transform3D* Transform;
        public PlayerLink* Link;
    }

    public override void Update(Frame f, ref Filter filter)
    {
        // gets the input for correct player
        Input input = *f.GetPlayerInput(filter.Link->Player);

        var inputVector = new FPVector2(input.Direction.X, input.Direction.Y);

        if(inputVector.SqrMagnitude != default)
        {
            filter.Transform->Rotation = FPQuaternion.LookRotation(inputVector.XOY);
        }

        filter.VehicleController3D->Move(f, filter.Entity, input.Direction.XOY);
    }
}