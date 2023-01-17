using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public float speed;
    private float cameraPositionY;
    private Camera cameraController;
    private Parallax[] parallaxes;
    private Vector3 lastCameraPos;
    private float sizeY;
    private float sizeX;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = Camera.main.GetComponent<Camera>();
        parallaxes = new Parallax[3];
        parallaxes[1] = GetComponentInChildren<Parallax>();

        Vector3 pos = parallaxes[1].transform.position;

        sizeX = parallaxes[1].GetComponent<SpriteRenderer>().bounds.size.x;
        sizeY = parallaxes[1].GetComponent<SpriteRenderer>().bounds.size.y;

        parallaxes[0] = Instantiate(parallaxes[1], new Vector3(pos.x - sizeX, pos.y - sizeY, pos.z), Quaternion.identity, transform);
        parallaxes[2] = Instantiate(parallaxes[1], new Vector3(pos.x + sizeY, pos.y + sizeY, pos.z), Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        // Create parallax effect if graphics is not set to low
        float moveY = lastCameraPos.y - cameraController.transform.position.y;
        float moveX = lastCameraPos.x - cameraController.transform.position.x;
        transform.position += Vector3.up * moveY * speed;
        transform.position += Vector3.right * moveX * speed;

        cameraPositionY = cameraController.transform.position.y;

        for (int i = 0; i < parallaxes.Length; i++) {
            float pos = parallaxes[i].transform.position.y;

            parallaxes[i].isCameraInPosition = cameraPositionY > pos - (sizeY / 2)
                && cameraPositionY < pos + (sizeY / 2);

            if (parallaxes[i].isCameraInPosition) {
                if (!parallaxes[i].isLastVisited) {
                    Vector3 currentpos = parallaxes[i].transform.position;
                    parallaxes[i].isLastVisited = true;

                    parallaxes[(i + 2) % 3].transform.position = currentpos - (Vector3.up * sizeY);
                    parallaxes[(i + 1) % 3].transform.position = currentpos + (Vector3.up * sizeY);
                }
            } else {
                parallaxes[i].isLastVisited = false;
            }
        }

        lastCameraPos = cameraController.transform.position;
    }

    // The calculation on the above method is derived from this

    //if (parallaxes[0].isCameraInPosition && currentPos != 1) {
    //    parallaxes[2].transform.position = parallaxes[0].transform.position - (Vector3.right * parallaxes[1].sizeX);
    //    parallaxes[1].transform.position = parallaxes[0].transform.position + (Vector3.right * parallaxes[1].sizeX);
    //    currentPos = 1;
    //}
    //else if (parallaxes[1].isCameraInPosition && currentPos != 0) {
    //    parallaxes[0].transform.position = parallaxes[1].transform.position - (Vector3.right * parallaxes[1].sizeX);
    //    parallaxes[2].transform.position = parallaxes[1].transform.position + (Vector3.right * parallaxes[1].sizeX);
    //    currentPos = 0;
    //}
    //else if (parallaxes[2].isCameraInPosition && currentPos != 2) {
    //    parallaxes[1].transform.position = parallaxes[2].transform.position - (Vector3.right * parallaxes[1].sizeX);
    //    parallaxes[0].transform.position = parallaxes[2].transform.position + (Vector3.right * parallaxes[1].sizeX);
    //    currentPos = 2;
    //}
}
