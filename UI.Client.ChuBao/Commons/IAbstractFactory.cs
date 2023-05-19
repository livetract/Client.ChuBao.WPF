namespace UI.Client.ChuBao.Commons
{
    public interface IAbstractFactory<T>
    {
        T Create();
    }
}