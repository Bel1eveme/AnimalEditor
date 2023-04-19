﻿namespace AnimalEditor.Model.Animals
{
    internal class Stork : Bird
    {
        public int BeakLength { get; set; } 
        public Stork(DateOnly birthDate, int maxFlightHeight, Nest nest, int beakLength) : base(birthDate, maxFlightHeight, nest)
        {
            BeakLength = beakLength;
        }

    }
}
