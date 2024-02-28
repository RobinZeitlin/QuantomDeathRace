using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    public unsafe class EnemyMovement : SystemMainThreadFilter<EnemyMovement.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public EnemyController* enemyController;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            filter.enemyController->MoveForward(f, filter.Entity, FPVector3.Zero);
        }
    }
}
