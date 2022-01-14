using UnityEngine;

namespace Features.Tutorial
{
    public abstract class TutorialState
    {
        public TutorialState PrevState { get; set; }
        public TutorialState NextState { get; set; }
        
        private readonly GameObject[] _gameObjects;
        

        protected TutorialState(GameObject[] gameObjects)
        {
            _gameObjects = gameObjects;
        }
        
        protected TutorialState(GameObject[] gameObjects, TutorialState prevState, TutorialState nextState)
        {
            _gameObjects = gameObjects;
            PrevState = prevState;
            NextState = nextState;
        }

        protected virtual void GoNext()
        {
            TearDown();
            
        }

        public virtual void Setup()
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.SetActive(true);
            }
        }
        
        public virtual void TearDown()
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.SetActive(false);
            }
        }
    }
}