using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public bool LandOnEntry;

    public TMP_Text Text;

    public GameObject Chair;
    public GameObject Helmet;
    public GameObject HelmetBase;

    public string MoonScene;
    public string MarsScene;
    public string EarthScene;
    public string ClassroomScene;

    public GameObject WarpEffect;

    public float TimeForHelmetMove = 0.3f;
    public float LaunchAccelerationTime = .5f;
    public float LaunchAcceleration = 20f;
    public float LaunchContinueMovementTime = 1f;
    public float LandAccelerationTime = .5f;
    public float LandAcceleration = 10f;
    public float LandContinueMovementTime = 1f;

    public float LandStartYOffet = 30f;

    private Vector3 helmetBeginPos;
    private float targetYPos;

    private void Start()
    {
        WarpEffect.SetActive(false);
        Helmet.SetActive(false);
        HelmetBase.SetActive(false);

        helmetBeginPos = Helmet.transform.position;

        targetYPos = Chair.transform.position.y;

        if (LandOnEntry)
        {
            Land();
        }
    }

    public void WriteInText()
    {
        StartCoroutine(WriteInTextSequence());
    }

    public void GoToMoon()
    {
        StartCoroutine(RunLaunch(MoonScene));
    }
    public void GoToMars()
    {
        StartCoroutine(RunLaunch(MarsScene));
    }

    public void GoToEarth()
    {
        StartCoroutine(RunLaunch(EarthScene));
    }

    public void GoToClassroom()
    {
        StartCoroutine(RunLaunch(ClassroomScene));
    }
    public void Land()
    {
        StartCoroutine(RunLand());
    }

    public void OnFullSequence()
    {
        StartCoroutine(RunFullSequence());
    }

    private IEnumerator WriteInTextSequence()
    {
        var charactersPerSec = 20;
        var allText = "Oh my gosh I must have felt so confused when the teacher and fall asleep when having the astronomy class, now everyone is left….Astronomy is so hard to grasp…..the moon, the solar system….";

        var numCharacters = 0;
        var timeWriting = 0f;

        while (numCharacters < allText.Length)
        {
            timeWriting += Time.deltaTime;
            numCharacters = (int)(timeWriting * (float)charactersPerSec);

            var textSoFar = allText.Substring(0, numCharacters);
            Text.text = textSoFar;
            yield return null;
        }

        Text.text = allText;
    }

    private IEnumerator RunFullSequence()
    {
        yield return RunLaunch(MoonScene);
        yield return RunLand();
    }

    private IEnumerator RunLaunch(string nextScene)
    {
        var moveTimeLeft = TimeForHelmetMove;
        var helmetEndPos = helmetBeginPos;
        var helmetStartPos = helmetEndPos;
        helmetStartPos.y += 1f;
        Helmet.transform.position = helmetStartPos;
        Helmet.SetActive(true);

        while (moveTimeLeft > 0)
        {
            moveTimeLeft -= Time.deltaTime;

            var t = (TimeForHelmetMove - moveTimeLeft) / TimeForHelmetMove;
            var curHelmetPos = Vector3.Lerp(helmetStartPos, helmetEndPos, t);
            Helmet.transform.position = curHelmetPos;
            yield return null;
        }

        Helmet.transform.position = helmetEndPos;
        HelmetBase.SetActive(true);

        // Launch chair, accelerate first
        var accelerateTimeLeft = LaunchAccelerationTime;
        var curSpeed = 0f;

        while (accelerateTimeLeft > 0f)
        {
            accelerateTimeLeft -= Time.deltaTime;
            curSpeed += LaunchAcceleration * Time.deltaTime;

            MoveChair(curSpeed);

            yield return null;
        }

        // Continue at same rate 
        var continueTimeLeft = LaunchContinueMovementTime;

        while (continueTimeLeft > 0)
        {
            continueTimeLeft -= Time.deltaTime;

            MoveChair(curSpeed);

            yield return null;
        }

        // Decelerate for same amount of time as acceleration
        var decelerationTimeLeft = LaunchAccelerationTime;

        while (decelerationTimeLeft > 0f)
        {
            decelerationTimeLeft -= Time.deltaTime;
            curSpeed -= LaunchAcceleration * Time.deltaTime;

            MoveChair(curSpeed);

            yield return null;
        }

        curSpeed = 0f;

        yield return DoTransition();

        SceneManager.LoadScene(nextScene);
    }

    private IEnumerator DoTransition()
    {
        // Apply warp effect
        var warpTimeLeft = 1.4f;
        WarpEffect.SetActive(true);

        while (warpTimeLeft > 0)
        {
            warpTimeLeft -= Time.deltaTime;
            yield return null;
        }

        // Change floor for moon
        WarpEffect.SetActive(false);
    }

    private IEnumerator RunLand()
    {
        Helmet.SetActive(true);

        var curPos = Chair.transform.position;
        curPos.y += LandStartYOffet;
        Chair.transform.position = curPos;

        // Accelerate downwards
        var accelerateTimeLeft = LandAccelerationTime;
        var curSpeed = 0f;

        while (accelerateTimeLeft > 0f)
        {
            accelerateTimeLeft -= Time.deltaTime;
            curSpeed -= LandAcceleration * Time.deltaTime;

            MoveChair(curSpeed);

            yield return null;
        }

        // Maintain speed downwards
        var continueTimeLeft = LandContinueMovementTime;

        while (continueTimeLeft > 0)
        {
            continueTimeLeft -= Time.deltaTime;

            MoveChair(curSpeed);

            yield return null;
        }

        // Decelerate
        var decelerationTimeLeft = LandAccelerationTime;

        while (Chair.transform.position.y > targetYPos)
        {
            decelerationTimeLeft -= Time.deltaTime;
            curSpeed += LandAcceleration * Time.deltaTime;
            if (curSpeed > -1f)
            {
                curSpeed = -1f;
            }

            MoveChair(curSpeed);

            yield return null;
        }

        // Remove helmet
        Helmet.transform.position = helmetBeginPos;
        var helmetStartPos = Helmet.transform.position;
        var helmetEndPos = helmetStartPos;
        helmetEndPos.y += 1f;
        HelmetBase.SetActive(false);
        var moveTimeLeft = TimeForHelmetMove;

        while (moveTimeLeft > 0)
        {
            moveTimeLeft -= Time.deltaTime;

            var t = (TimeForHelmetMove - moveTimeLeft) / TimeForHelmetMove;
            var curHelmetPos = Vector3.Lerp(helmetStartPos, helmetEndPos, t);
            Helmet.transform.position = curHelmetPos;
            yield return null;
        }

        Helmet.SetActive(false);
    }

    private void MoveChair(float curSpeed)
    {
        var curChairPos = Chair.transform.position;

        curChairPos.y += curSpeed * Time.deltaTime;

        if (curChairPos.y < targetYPos)
        {
            curChairPos.y = targetYPos;
        }

        Chair.transform.position = curChairPos;
    }
}
