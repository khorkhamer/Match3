using System.Collections.Generic;

namespace Match3
{
    public class GameState
    {
        private List<GameObject> _state;
        public List<GameObject> Get() => _state;

        public void Add(GameObject go)
        {
            _state.Add(go);
        }

        public GameState()
        {
            _state = new List<GameObject>(100);
        }

        public bool Contains((int,int) coord)
        {
            foreach (var elem in _state)
            {
                if (elem.GetLogicalCoord() == coord)
                    return true;
            }

            return false;
        }
    }
}