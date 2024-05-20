using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasHandler : MonoBehaviour{

    AccessVariable AccessVariableScript;

    [SerializeField] RectTransform itemShowPanelTranform;
    [SerializeField] RectTransform ItemShowContainer;

    [SerializeField] GameObject itemShowPanel;
    [SerializeField] GameObject ItemShowContainerGameObject;

    [SerializeField] GameObject MsgPanel;

    int itemShowPanelSize, ItemShowContainerChildSize;
    float itemShowPanelTranformSize, ItemShowContainerTrafromSize;

    [SerializeField] GameObject ZombiePanel;

    void Start(){
        
        itemShowPanelTranform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
    }
    void Awake(){
        itemShowPanelSize = 0;
        itemShowPanelTranformSize = 50;
        ItemShowContainerTrafromSize = 60;

        AccessVariableScript = GameObject.Find("All Access Variable").GetComponent<AccessVariable>();
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (AccessVariableScript.GameisPaues) { resume(); }
            else { Pause(); }
        }    
    }
    public void UpdateSize() {
        
        if (itemShowPanelSize <= 3) {
            
            if(itemShowPanel.activeSelf == false) { itemShowPanel.SetActive(true); }

            float Size = itemShowPanelTranformSize * itemShowPanelSize;
            itemShowPanelTranform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Size);
        }
        else if(itemShowPanelSize > 3) {
            ItemShowContainerChildSize = ItemShowContainerGameObject.transform.childCount - 3;
            float Size = ItemShowContainerTrafromSize * ItemShowContainerChildSize;
            ItemShowContainer.anchoredPosition = Vector2.down * Size;
            //print($" {Size} ,-> {Vector2.up * Size}, -> {ItemShowContainer.anchoredPosition}");
        }
    }

    public int AdditemShowPanelSize() {
        itemShowPanelSize++;
        UpdateSize();
        return itemShowPanelSize;
    }

    public int ReduceitemShowPanelSize(GameObject hideBtn) {
        itemShowPanelSize--;
        UpdateSize();
        if (itemShowPanelSize == 0) { itemShowPanel.SetActive(false); hideBtn.SetActive(false); }
        return itemShowPanelSize;
    }

    public void Hide() {
        if (itemShowPanel.activeSelf) {
            itemShowPanel.SetActive(false);
        }
        else {
            itemShowPanel.SetActive(true);
        }
    }

    public  void showmessage(string msg = "Intract to press Alt") {
        TMP_Text mgsShow = MsgPanel.GetComponentInChildren<TMP_Text>();
        mgsShow.text = msg;
    }

    void resume() {
        Time.timeScale = 1;
        AccessVariableScript.AnimationHandlerScript.PauseAndResume(0);
        AccessVariableScript.GameisPaues = false;
    }
    void Pause() {
        Time.timeScale = 0;
        AccessVariableScript.GameisPaues = true;
        AccessVariableScript.AnimationHandlerScript.PauseAndResume(1);
    }

    public void QuitBtn() { Application.Quit(); } 
    public void NewGame() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

}
