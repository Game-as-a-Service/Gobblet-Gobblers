using System;
using System.Collections.Generic;
using Gaas.GobbletGobblers.Domain.Enums;
using Gaas.GobbletGobblers.Domain.Sizes;

namespace Gaas.GobbletGobblers.Domain
{
    public class Cock : IComparable<Cock>
    {
        public Player? Owner { get; set; }

        public Color Color { get; private set; }

        public Size Size { get; private set; }


        public Cock(Color color, Size size)
        {
            Color = color;
            Size = size;
        }

        public int CompareTo(Cock? other)
        {
            if (other == null)
                return 1;

            return this.Size.CompareTo(other.Size);
        }

        public static IEnumerable<Cock> StandardEditionCocks(Color color)
        {
            return new List<Cock>
            {
                new Cock(color, new Small()),
                new Cock(color, new Small()),
                new Cock(color, new Medium()),
                new Cock(color, new Medium()),
                new Cock(color, new Large()),
                new Cock(color, new Large()),
            };
        }
    }
}
