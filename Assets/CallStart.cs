using UnityEngine;

public class CallStart : MonoBehaviour
{

    TallyCountManager _tally;

    private void Update()
    {
        if(_tally == null)
        {
            _tally = GameObject.FindAnyObjectByType<TallyCountManager>();

            _tally.SetStartCounts();
        }
    }
}
