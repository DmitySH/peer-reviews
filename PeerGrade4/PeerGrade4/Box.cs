namespace PeerGrade4
{
    class Box
    {
        public double Weight { get; }
        public double KgPrice { get; }
        public double FullPrice { get; }

        /// <summary>
        /// Constructor for boxes.
        /// </summary>
        /// <param name="weight">Weight of a box.</param>
        /// <param name="price">Price for kg of a box.</param>
        public Box(double weight,double price)
        {
            Weight = weight;
            KgPrice = price;
            FullPrice = weight * KgPrice;
        }
    }
}
