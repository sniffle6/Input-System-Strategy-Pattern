namespace _InputTest.WorldObjects.Scripts
{
    public interface IOpenable
    {
        bool Opened { get; set; }
        
        void Open();
        void Close();
    }
}