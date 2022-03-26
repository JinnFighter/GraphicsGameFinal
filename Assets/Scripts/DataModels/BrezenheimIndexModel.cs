namespace Pixelgrid.DataModels
{
    public class BrezenheimIndexModel
    {
        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                IndexChangedEvent?.Invoke(_index);
            }
        }

        public delegate void IndexChanged(int index);

        public event IndexChanged IndexChangedEvent;
    }
}
