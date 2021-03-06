﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.World.Games;
using Legends.Protocol.GameClient.Enum;

namespace Legends.Scripts.Maps
{
    public class SummonersRiftScript : MapScript
    {
        public const MapIdEnum MAP_ID = MapIdEnum.SummonersRift;

		public override float GoldsPerSeconds
		{
			get
			{
				return 2.04f;
			}
		}
		
        public SummonersRiftScript(Game game) : base(game)
        {

        }
        /// <summary>
        /// Called when GameTime = 0. And Callback start.
        /// </summary>
        public override void OnStart()
        {
            Announce(AnnounceEnum.WelcomeToSR, 25);
            Announce(AnnounceEnum.ThirySecondsToMinionsSpawn, 35);
            Announce(AnnounceEnum.MinionsHaveSpawned, 65);
        }

        public override void OnSpawn()
        {
          
        }

        public override void CreateBindings()
        {
         

        }
    }
}
