using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Quantum.CarGame.MovementSystem;

namespace Quantum.CarGame
{

    public unsafe class EnemySpawnSystem : SystemMainThreadFilter<EnemySpawnSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public EnemySpawner* spawner;
        }

        private FP delayDuration = 1;
        private FP timeSinceLastSpawn = FP._0;

        public override void Update(Frame f, ref Filter filter)
        {
            timeSinceLastSpawn += f.DeltaTime;

            if (timeSinceLastSpawn >= delayDuration)
            {
                timeSinceLastSpawn = FP._0;

                FPVector2 spawnPosition = new FPVector2(1, 1);
                var prototypeId = filter.spawner->prototype.Id;

                Log.Debug("Spawning enemy");

                SpawnEntity(f, prototypeId, spawnPosition);
            }
        }

        private static void SpawnEntity(Frame frame, AssetGuid prototypeId, FPVector2 position)
        {
            EntityPrototype prototype = frame.FindAsset<EntityPrototype>(prototypeId);

            EntityRef entity = frame.Create(prototype);

            Log.Debug($"Spawned entity {entity} at position {position}");
        }

    }
}
