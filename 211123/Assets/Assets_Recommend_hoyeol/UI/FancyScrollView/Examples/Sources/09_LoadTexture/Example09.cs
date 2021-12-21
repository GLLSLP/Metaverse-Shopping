﻿/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using UnityEngine;

namespace FancyScrollView.Example09
{
    class Example09 : MonoBehaviour
    {

        [SerializeField] ScrollView scrollView = default;
        string  query_result;
        string[] dot_split;
        string[] info_item;
        void Start()
        {
            query_result = "125cm(50인치) 4K UHD TV.KU50UT8090FXKR.Stand.50.UHD.삼성전자.None.1,190,000원.https:.125cm(50인치) UHD TV.KU50UA7070FXKR.Stand.50.UHD.삼성전자.None.1,090,000원.https://www.etlandmall.co.kr/pc/product/product.do?prdMstCd=S3793716&cateIdx=1001.";//RecDB.result;
            info_item = query_result.Split('\n');
            dot_split = query_result.Split('.');
        ItemData[] itemData =
        {
            new ItemData(
                dot_split[0],    //www.downloadHandler.text
                "LG전자 OLED77A1MNA 194cm(77인치) OLED TV\t5,190,000원",
                "https://m.etlandmall.co.kr/nas/cdn/attach/product/2021/06/14/S3308023/S3308023_0_500.jpg"
            ),
            new ItemData(
                "상품번호 S2974243",
                "[삼성전자/KQ75QNA90AFXKR] 189cm(75인치) Neo QLED TV\t6,899,000원",
                "https://m.etlandmall.co.kr/nas/cdn/attach/product/2021/03/22/S2974243/S2974243_0_500.png"
            ),
            new ItemData(
                "상품번호 S2987021",
                "[삼성전자/KQ85QA70AFXKR] 214cm(85인치) QLED TV\t7,300,000원",
                "https://m.etlandmall.co.kr/nas/cdn/attach/product/2021/03/25/S2987021/S2987021_0_500.jpg"
            )/*,
            new ItemData(
                "상품번호 S3121113",
                "LG전자/OLED48C1KNB 120cm(48인치) 4세대 알파9\t1,540,000원",
                "https://m.etlandmall.co.kr/nas/cdn/attach/product/2021/04/28/S3121113/S3121113_0_500.jpg"
            ),
            new ItemData(
                "상품번호 S0167913",
                "[아남/FDL430CT] LED TV / 109cm(43인치) / FHD (스탠드형) 서울/경기한정배송\t449,000",
                "https://m.etlandmall.co.kr/nas/cdn/attach/product/2020/10/14/S0167913/S0167913_0_500.jpg"
            )*/
        };
            scrollView.UpdateData(itemData);
        }
    }
}
