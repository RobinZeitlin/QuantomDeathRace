using Photon.Deterministic;
using Quantum.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    public unsafe partial struct EnemyController
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MoveForward(FrameBase frame, EntityRef entity, FPVector3 target)
        {
            Transform3D* tBody = null;

            if (frame.Unsafe.TryGetPointer(entity, out tBody) == false) { return; }

            target = ClosestPlayer(frame, tBody);
            tBody->LookAt(target);
            tBody->Position += tBody->Forward * 5 * frame.DeltaTime;
        }
       
        public FPVector3 ClosestPlayer(FrameBase frame, Transform3D* tBody)
        {
            List<Transform3D> allPlayers = new List<Transform3D>();
            var filtered = frame.Filter<Transform3D, VehicleController3D>();

            while(filtered.Next(out var entityRef, out var transform, out var vehicle))
            {
                allPlayers.Add(transform);
            }

            return CalculateClosest(allPlayers, tBody);
            
        }

        public FPVector3 CalculateClosest(List<Transform3D> players, Transform3D* tBody)
        {
            FP closestDistance = FP.MaxValue;
            Transform3D closestPlayer = new Transform3D();

            foreach (var player in players)
            {
                FP distance = FPVector3.Distance(player.Position, tBody->Position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlayer = player;
                }
            }

            return closestPlayer.Position;
        }
  
    }
}
