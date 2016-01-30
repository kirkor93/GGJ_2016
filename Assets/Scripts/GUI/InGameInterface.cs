﻿using System;
using UnityEngine;
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

        protected void Start()
        {
            Goat.OnGameOver += HandleOnGameOver;
        }

        private void HandleOnGameOver(object sender, GameOverEventArgs gameOverEventArgs)
        {
            
        }

        protected void Update()
        {
            GoatScore.text = string.Format("Score: {0}", Goat.CurrentScore);
            ChickenScore.text = string.Format("Score: {0}", Chicken.CurrentScore);

            GoatHealthBar.fillAmount = (float) Goat.HitPoints/Goat.MaxHitPoints;
            ChickenHealthBar.fillAmount = (float) Chicken.HitPoints/Chicken.MaxHitPoints;
        }
    }
}
