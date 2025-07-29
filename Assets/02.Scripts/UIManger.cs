using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManger : MonoBehaviour
{
    [Header("��� UI")]
    public TMP_Text moneyTxt;       // ���� �ݾ׸�ŭ �����ϰ� �� ������ ���
    // ShowDown���� �� ����Ŭ ���� Round1���� 2,3 ������ ��� ����
    public TMP_Text roundTxt;
    // Pre-Flop > Flop > Turn > River > ShowDown ������ �� �Ͽ� �°� ����
    // �÷��̾ LeaveGame ���� ������ �ϸ� Round, Turn�� �ʱ� ���·� ����
    public TMP_Text turnTxt;
    [Header("Btn ��ȣ�ۿ�")]
    public Canvas DeckInfo;
    public RectTransform ruleBG;
    public RectTransform betBG;     // Rule UI ������ ��Ȱ��ȭ
    public Image firstRulePage;
    public Image secondRulePage;
    public Image thirdRulePage;
    public TMP_InputField betInput;
    public TMP_InputField raiseInput;

    public void OnClickDeckGuide()
    {

    }

    public void OnClickLeaveGame()
    {

    }

    public void OnClickGameRule()
    {

    }

    public void OnClickFold()
    {

    }

    public void OnClickCheck()
    {

    }

    public void OnClickBet()
    {

    }

    public void OnClickCall()
    {

    }

    public void OnClickRaise()
    {

    }

    public void OnClickRuleExit()
    {

    }

    public void OnClickPrevPage()
    {

    }

    public void OnClickNextPage()
    {

    }
}
