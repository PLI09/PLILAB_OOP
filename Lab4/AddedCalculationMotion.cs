using Model;

namespace Lab4
{
    public class AddedCalculationMotion : EventArgs
    {
        public MotionBase Motion { get; }

        public AddedCalculationMotion(MotionBase motion)
        {
            Motion = motion ?? throw new ArgumentNullException(nameof(motion));
        }
    }
}
