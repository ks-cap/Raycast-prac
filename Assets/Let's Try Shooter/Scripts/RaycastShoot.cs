using System.Collections;
using UnityEngine;

public class RaycastShoot : MonoBehaviour {

    public int gunDamage = 1;
    // 再度銃を発砲するまでの待機時間
    public float fireRate = .25f;
    // 武器（レイキャスト）の範囲
    public float weaponRange = 50f;
    public float hitForce = 100f;
    // マークするゲームオブジェクトの最後の位置
    public Transform gunEnd;

    private Camera fpsCam;
    // レーザの見える時間
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    // 直線
    private LineRenderer laserline;
    // 再発砲待機時間
    private float nextFire;

	void Start () {
        laserline = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
	}
	
	
	void Update () {
        // Fireボタンが押され, かつ十分な時間が経過すれば発砲する
        if (Input.GetButtonDown("Fire1") Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());
            // 光線の原点（画面の中心）
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            // 光線がオブジェクトに当たったとき返される情報
            RaycastHit hit;
            // レーザーラインの開始位置と終了位置
            laserline.SetPosition(0, gunEnd.position);
        }
	}
    private IEnumerator ShotEffect()
    {
        gunAudio.Play();
        // 線を表示
        laserline.enabled = true;
        // 0.07秒間待つことになる
        yield return shotDuration;
        // 線を非表示
        laserline.enabled = false;
    }
}
