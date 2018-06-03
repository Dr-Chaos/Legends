﻿using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Messages.Game;
using Legends.World.Entities.Movements;
using Legends.World.Games;
using Legends.World.Games.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities
{
    public abstract class Unit
    {
        public const float DEFAULT_MODEL_SIZE = 1f;

        public int NetId
        {
            get;
            set;
        }
        public Team Team
        {
            get;
            private set;
        }
        public abstract string Name
        {
            get;
        }
        /// <summary>
        /// Numero de l'unitée dans son équipe.
        /// </summary>
        public int TeamNo
        {
            get;
            set;
        }
        public abstract bool IsMoving
        {
            get;
        }
        public string Model
        {
            get;
            protected set;
        }
        public int SkinId
        {
            get;
            protected set;
        }
        private List<Action> SynchronizedActions
        {
            get;
            set;
        }
        public Unit()
        {
            SynchronizedActions = new List<Action>();
        }

        public Vector2 Position
        {
            get;
            set;
        }

       
        public Game Game
        {
            get;
            private set;
        }
        public abstract float PerceptionBubbleRadius
        {
            get;
        }

        public virtual void Initialize()
        {

        }
        public virtual void UpdateModel(string newModel, bool updateSpells, int skinId)
        {
            Model = newModel;
            SkinId = skinId;
            Game.Send(new UpdateModelMessage(NetId, newModel, updateSpells, skinId));
        }
        /// <summary>
        /// Called by Game.AddUnit()
        /// </summary>
        /// <param name="team"></param>
        public void DefineTeam(Team team)
        {
            this.Team = team;
        }
        public void DefineGame(Game game)
        {
            this.Game = game;
        }
        public void Invoke(Action action)
        {
            SynchronizedActions.Add(action);
        }
        public virtual void Update(long deltaTime)
        {
            for (int i = 0; i < SynchronizedActions.Count; i++)
            {
                SynchronizedActions[i].Invoke();
            }

            SynchronizedActions.Clear();
        }
        public abstract void OnUnitEnterVision(Unit unit);

        public abstract void OnUnitLeaveVision(Unit unit);

        public Team GetOposedTeam()
        {
            return Team.Id == TeamId.BLUE ? Game.PurpleTeam : Game.BlueTeam;
        }
        public bool InFieldOfView(Unit other)
        {
            var dist = this.GetDistanceTo(other);
            return dist <= PerceptionBubbleRadius;
        }
        public float GetDistanceTo(Unit other)
        {
            return (float)Math.Sqrt(Math.Pow(other.Position.X - Position.X, 2) + Math.Pow(other.Position.Y - Position.Y, 2));
        }
        public bool HasVision(Unit other)
        {
            return Team.HasVision(other);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}