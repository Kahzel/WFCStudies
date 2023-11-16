namespace WaveFunctionCollapse
{
    public interface IOutputCreator<T>
    {
        T OutputImage { get; }
        void CreateOutput(PatternManager manager, int[][] outputvalues, int width, int height);
    }

}