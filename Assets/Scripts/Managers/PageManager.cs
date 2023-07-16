using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public enum Pages
    {
        None,
        MainMenu,
        GamePanel,
        GameOverPanel,
        PausePanel
    }

    public class PageManager : MonoBehaviour
    {
        public DictionaryUnity<Pages, GameObject> pages;
        private Pages _currentPage;

        public Pages CurrentPage
        {
            get => _currentPage;
            set
            {
                PreviousPages.Add(_currentPage);
                _currentPage = value;
            }
        }

        public List<Pages> PreviousPages { get; private set; }

        public static PageManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            _currentPage = Pages.MainMenu;
        }

        private void Start()
        {
            PreviousPages = new List<Pages>();
        }


        public void ShowPage(Pages page, bool hideActivePage = true)
        {
            if (hideActivePage)
                HidePage(_currentPage);
            _currentPage = page;
            pages[page].SetActive(true);
        }

        public void HidePage(Pages page)
        {
            PreviousPages.Add(page);
            pages[page].SetActive(false);
        }

        public void GoBack()
        {
            var page = PreviousPages.Last();
            PreviousPages.Remove(page);

            pages[_currentPage].SetActive(false);
            _currentPage = page;
            pages[page].SetActive(true);
        }
    }
}