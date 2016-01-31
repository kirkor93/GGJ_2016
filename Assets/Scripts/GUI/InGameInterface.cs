using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class InGameInterface : MonoBehaviour
    {
        public Goat Goat;
        public Chicken Chicken;

        public Image GoatHealthBar;
        public Image ChickenHealthBar;
        public Text GoatScore;
        public Text ChickenScore;
        public RectTransform GameOverScreen;
        public Image GoatFillBar;

        private RectTransform _barParent;
        private RectTransform _canvasRect;
        private Camera _sceneCamera;

        protected void Start()
        {
            Goat.OnGameOver += HandleOnGameOver;
            Chicken.OnGameOver += HandleOnGameOver;
            _barParent = GoatFillBar.transform.parent.GetComponent<RectTransform>();
            _canvasRect = GetComponent<RectTransform>();
            _sceneCamera = FindObjectOfType<Camera>();
            Debug.Log(FindObjectsOfType<Camera>().Length);
        }

        private void HandleOnGameOver(object sender, GameOverEventArgs gameOverEventArgs)
        {
            bool onCompleteAdded = false;
            foreach (Image ren in GameOverScreen.GetComponentsInChildren<Image>(true))
            {
                Color c = ren.color;
                float targetAlpha = c.a;
                c.a = 0.0f;
                ren.color = c;
                Tweener tween = ren.DOFade(targetAlpha, 4.0f);
                if (!onCompleteAdded)
                {
                    onCompleteAdded = true;
                    tween.OnComplete(() => SceneLoader.Instance.LoadLevel(SceneManager.GetActiveScene().name));
                }
            }
            GameOverScreen.gameObject.SetActive(true);
        }

        protected void Update()
        {
            float goatThrowForce = Goat.CurrentThrowForce/Goat.ThrowForce;
//            _barParent.gameObject.SetActive(Goat.HoldingChicken);
            if (Goat.HoldingChicken)
            {
                GoatFillBar.fillAmount = goatThrowForce;
            }
            else
            {
                GoatFillBar.fillAmount = 0.0f;
            }

            GoatScore.text = string.Format("Score: {0}", Goat.CurrentScore);
            ChickenScore.text = string.Format("Score: {0}", Chicken.CurrentScore);

            GoatHealthBar.fillAmount = (float) Goat.HitPoints/Goat.MaxHitPoints;
            ChickenHealthBar.fillAmount = (float) Chicken.HitPoints/Chicken.MaxHitPoints;
        }
    }
}
