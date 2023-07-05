using UnityEngine;
using System.Collections.Generic;

namespace Fade.LevelManager
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        public int levelNumber = 0;
        public List<GameObject> levels;
        public GameObject openLevel;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            DontDestroyOnLoad(this.gameObject);

            if (PlayerPrefs.HasKey("levelNumber"))
            {
                levelNumber = PlayerPrefs.GetInt("levelNumber");
                var level = Instantiate(levels[levelNumber]);
                openLevel = level;
            }

            else LoadNextLevel();
        }

        [ContextMenu("LOAD NEXT LEVEL")]
        public void LoadNextLevel()
        {
            if (openLevel != null) DeleteLevel();

            var scene = Instantiate(levels[levelNumber]);
            openLevel = scene;

            PlayerPrefs.SetInt("levelNumber", levelNumber);
            PlayerPrefs.Save();

            levelNumber++;
        }

        [ContextMenu("DELETE LEVEL")]
        public void DeleteLevel()
        {
            if (openLevel != null) Destroy(openLevel);
            else Debug.LogWarning("Current Level Is Empty!!");
        }
    }
}