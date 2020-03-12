namespace _InputTest.WorldObjects.Scripts
{
    public interface IOpenenable
    {
        bool Opened { get; set; }
        
        void Open();
        void Close();
    }
}