namespace Bayes.Builders
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}