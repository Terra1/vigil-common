﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Vigil
{
    public enum ShipType { Frigate, Corvette, Battleship, Carrier, Titan }
    public class ShipMovements
    {
        public Vector2 VelocityChange { get; set; } = new Vector2(0, 0);
        public float ThrustChange { get; set; } = 0f;
        public float SpinChange { get; set; } = 0f;
    }
    public class Ship
    {
        private Vector2 _Velocity;
        private float _Angle = 0.0f;
        private float _Spin = 0.0f;
        private float _Thrust = 0.0f;
        private ShipType _Type;

        public Ship( ShipType type )
        {
            _Type = type;
        }
        public ShipType GetShipType()
        {
            return _Type;
        }
        public Vector2 GetVelocity()
        {
            return _Velocity;
        }
        public float GetAngle()
        {
            return _Angle;
        }
        public float GetSpin()
        {
            return _Spin;
        }
        public float GetThrust()
        {
            return _Thrust;
        }

        internal void UpdateMoves(ShipMovements shipMove)
        {
            _Thrust = Math.Max(_Thrust + shipMove.ThrustChange, 0.0f);
            _Spin += shipMove.SpinChange;
            _Angle += _Spin;
            _Velocity += TranslateThrustAngleToMovement(_Thrust, _Angle);
        }

        internal Vector2 TranslateThrustAngleToMovement(float Thrust, float Angle)
        {
            float AdjustedAngle = Angle - (float)Math.PI / 2f;
            Vector2 Adjustment = new Vector2(0, 0);
            Adjustment.X = Thrust * (float)Math.Cos(AdjustedAngle);
            Adjustment.Y = Thrust * (float)Math.Sin(AdjustedAngle);
            return Adjustment;
        }
    }
}
