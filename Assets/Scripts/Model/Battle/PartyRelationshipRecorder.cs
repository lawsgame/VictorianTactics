using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Opponents
{
    public int party1;
    public int party2;
}

public class PartyRelationshipRecorder
{
    private bool[,] _partyMatrix;


    private PartyRelationshipRecorder() {
        _partyMatrix = null;
    }

    public static PartyRelationshipRecorder create(int numberOfParties, List<Opponents> opponents)
    {
        PartyRelationshipRecorder model = new PartyRelationshipRecorder();
        model.ResetPartyMatrix(numberOfParties);
        foreach (Opponents opp in opponents)
            model.SetRelationShip(opp.party1, opp.party2, true);
        return model;
    }

    public void ResetPartyMatrix(int numberOfParties) {
        if (numberOfParties > 0)
            _partyMatrix = new bool[numberOfParties, numberOfParties];
        else
            Debug.LogErrorFormat("Reseting party matrix with value below 1: {0}", numberOfParties);
    }


    public void SetRelationShip(int party1, int party2, bool enemy)
    {
        if(IsPartyNumberExist(party1) && IsPartyNumberExist(party2))
        {
            _partyMatrix[party1, party2] = enemy;
            _partyMatrix[party2, party1] = enemy;
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
            return _partyMatrix[party1, party2];
        }
        else
        {
            Debug.LogErrorFormat("Set relationship between unknown party numbers; {0}, {1} ", party1, party2);
        }
        return false;
    }

    public bool IsPartyNumberExist(int partyNumber) => _partyMatrix != null && partyNumber < _partyMatrix.Length && partyNumber >= 0;
}


