using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyRelationshipRecorder : MonoBehaviour
{
    
    [SerializeField] private int parties;
    [SerializeField] private List<Opponents> opponents;

    private bool[,] _opponentMatrix;

    void Awake() { 
        ResetPartyMatrix(parties);
        foreach (Opponents opp in opponents)
            SetRelationShip(opp.party1, opp.party2, true);
    }

    public void ResetPartyMatrix(int numberOfParties) {
        if (numberOfParties > 0)
            _opponentMatrix = new bool[numberOfParties, numberOfParties];
        else
            Debug.LogErrorFormat("Reseting party matrix with value below 1: {0}", numberOfParties);
    }


    public void SetRelationShip(int party1, int party2, bool enemy)
    {
        if(IsPartyNumberExist(party1) && IsPartyNumberExist(party2))
        {
            _opponentMatrix[party1, party2] = enemy;
            _opponentMatrix[party2, party1] = enemy;
        }
        else
        {
            Debug.LogErrorFormat("Set relationship between unknown party numbers; {0}, {1} ", party1, party2);
        }
    }


    public bool AreOpposed(int party1, int party2)
    {
        if (IsPartyNumberExist(party1) && IsPartyNumberExist(party2))
        {
            return _opponentMatrix[party1, party2];
        }
        else
        {
            Debug.LogErrorFormat("Set relationship between unknown party numbers; {0}, {1} ", party1, party2);
        }
        return false;
    }

    public bool IsPartyNumberExist(int partyNumber) => _opponentMatrix != null && partyNumber < _opponentMatrix.Length && partyNumber >= 0;

    public bool SameSide(int party1, int party2)
    {
        if (IsPartyNumberExist(party1) && IsPartyNumberExist(party2))
        {
            return !_opponentMatrix[party1, party2];
        }
        else
        {
            Debug.LogErrorFormat("Set relationship between unknown party numbers; {0}, {1} ", party1, party2);
        }
        return false;
    }
}

[System.Serializable]
public class Opponents
{
    public int party1;
    public int party2;
}

