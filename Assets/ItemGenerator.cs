using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //conePrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private float startPos = 80;
    //ゴール地点
    private float goalPos = 360;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    private GameObject unitychan;
    //アイテムが出る位置
    private float generatePos;
    //アイテムを出す周期
    private float interval = 0;
    //unitychanの位置を記憶する変数
    private float beforeUnitychan=0;
    //unitychanが進んだ距離を測る変数
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        this.unitychan = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {
        //アイテムを出す位置は、unitychanの40m先
        this.generatePos = this.unitychan.transform.position.z + 40;

        //現在と1フレーム前のunitychanの位置から、進んだ距離を計測する
        distance=this.unitychan.transform.position.z - beforeUnitychan;
        
        beforeUnitychan = this.unitychan.transform.position.z;

        //アイテムを出す周期にunitychanの進んだ距離を参照するため、移動距離を加算する
        this.interval += distance;

        //周期は15mごとに設定
        if (interval>=15)
        {
            if (startPos <= generatePos && generatePos <= goalPos)
            {
                int num = Random.Range(1, 11);
                if (num <= 2)
                {
                    //コーンをx軸方向に一直線に生成
                    for (float j = -1; j <= 1; j += 0.4f)
                    {
                        GameObject cone = Instantiate(conePrefab);
                        cone.transform.position = new Vector3(4 * j, cone.transform.position.y, generatePos);
                    }
                }
                else
                {
                    //レーンごとにアイテムを生成
                    for (int j = -1; j <= 1; j++)
                    {
                        //アイテムの種類を決める
                        int item = Random.Range(1, 11);
                        //アイテムを置くz座標のオフセットをランダムに設定
                        int offsetZ = Random.Range(-5, 6);
                        //60%コイン配置:30%車配置:10%何もなし
                        if (1 <= item && item <= 6)
                        {
                            GameObject coin = Instantiate(coinPrefab);
                            coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, generatePos + offsetZ);
                        }
                        else if (7 <= item && item <= 9)
                        {
                            GameObject car = Instantiate(carPrefab);
                            car.transform.position = new Vector3(posRange * j, car.transform.position.y, generatePos + offsetZ);
                        }
                    }
                }
                //周期を0に戻す
                this.interval = 0;
            }
        }
    }
}
