﻿using Nethereum.Contracts;
using Nethereum.JsonRpc.UnityClient;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/* contract
 * 
pragma solidity ^0.4.0;
contract Sum
{
    function add(int256 num1, int256 num2) pure public returns(int256)
    {
        return num1 + num2;
    }
    
}
 * */
using Nethereum.Signer;

public class Main : MonoBehaviour
{
	private string m_Url = "http://0.0.0.0:8545";
	private string m_SenderAddress = "SENDER_ADDRESS"; 
	private string m_ContractAddress = "CONTRACT_ADDRESS";
	private string m_PrivateKey = "PRIVATE_KEY";
    private Contract m_Contract;
	private static string m_ABI = @"[{'constant':false,'inputs':[{'name':'_msg','type':'string'}],'name':'setMessage','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'message','outputs':[{'name':'','type':'string'}],'payable':false,'type':'function'}]";

    private string m_Input1 = "";
    private string m_Result = "";

    [SerializeField] private Button m_Btn = null;

    private void Start()
    {
        this.m_Contract = new Contract(null, m_ABI, m_ContractAddress);
    }

	IEnumerator CallAdd(string name)
    {
		var function = m_Contract.GetFunction("setMessage");
		var _TransactionInput = function.CreateTransactionInput (m_SenderAddress, new HexBigInteger(2000000), new HexBigInteger(0), name);

		var _Request = new TransactionSignedUnityRequest(m_Url, m_PrivateKey, m_SenderAddress);
		yield return _Request.SignAndSendTransaction(_TransactionInput);
		var _Result = _Request.Result;

		Debug.Log("_AddResult: " + _Result);
		m_Result = _Result.ToString();
        m_Btn.interactable = true;
    }

    public void OnClickAddBtn()
    {
        m_Btn.interactable = false;
        string _Name = m_Input1;
		StartCoroutine(CallAdd(_Name));
    }

    void OnGUI()
    {
        GUI.skin.textField.fontSize = 40;
        m_Input1 = GUI.TextField(new Rect(10, 100, 200, 45), m_Input1, 25);

        GUI.skin.label.fontSize = 18;
        GUI.Label(new Rect(10, 300, 800, 40), m_Result);
    }
}

//using Nethereum.Contracts;
//using Nethereum.JsonRpc.UnityClient;
//using Nethereum.RPC.Eth.DTOs;
//using System;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.UI;

///* contract
// * 
//pragma solidity ^0.4.0;
//contract Sum
//{
//    function add(int256 num1, int256 num2) pure public returns(int256)
//    {
//        return num1 + num2;
//    }

//}
// * */

//public class Main : MonoBehaviour
//{
//    private string m_Url = "https://rinkeby.infura.io";
//    private string m_ContractAddress = "0x97ad730824221ebc6d729f88f7ab36c04cee146a";
//    private Contract contract;
//    private static string m_ABI = @"[{'constant':true,'inputs':[{'name':'num1','type':'int256'},{'name':'num2','type':'int256'}],'name':'add','outputs':[{'name':'','type':'int256'}],'payable':false,'stateMutability':'pure','type':'function'}]";

//    [SerializeField] private Button m_Btn = null;

//    private void Start()
//    {
//        Debug.Log("Start");
//        this.contract = new Contract(null, m_ABI, m_ContractAddress);

//        //StartCoroutine(CallAdd());
//    }

//    IEnumerator CallAdd(int num1 = 1, int num2 = 2)
//    {
//        var _AddRequest = new EthCallUnityRequest(m_Url);
//        yield return _AddRequest.SendRequest(GetSumCallInput(num1, num2), Nethereum.RPC.Eth.DTOs.BlockParameter.CreateLatest());
//        var _AddResult = _AddRequest.Result;

//        Debug.Log("_AddResult: " + _AddResult);
//        m_Result = _AddResult.ToString();
//        m_Btn.interactable = true;
//    }

//    private CallInput GetSumCallInput(int num1, int num2)
//    {
//        var function = contract.GetFunction("add");
//        return function.CreateCallInput(num1, num2);
//    }

//    public void OnClickAddBtn()
//    {
//        m_Btn.interactable = false;
//        int _Num1 = Convert.ToInt32(m_Input1);
//        int _Num2 = Convert.ToInt32(m_Input2);
//        StartCoroutine(CallAdd(_Num1, _Num2));
//    }

//    string m_Input1 = "";
//    string m_Input2 = "";
//    string m_Result = "";
//    void OnGUI()
//    {
//        GUI.skin.textField.fontSize = 40;
//        m_Input1 = GUI.TextField(new Rect(10, 100, 200, 45), m_Input1, 25);
//        m_Input2 = GUI.TextField(new Rect(300, 100, 200, 45), m_Input2, 25);

//        GUI.skin.label.fontSize = 40;
//        GUI.Label(new Rect(245, 100, 40, 40), "+");

//        GUI.skin.label.fontSize = 18;
//        GUI.Label(new Rect(10, 300, 800, 40), m_Result);
//    }
//}
