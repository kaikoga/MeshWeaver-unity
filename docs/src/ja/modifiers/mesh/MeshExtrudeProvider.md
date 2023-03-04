# MeshExtrudeProvider

MeshExtrudeProviderはメッシュのポリゴンを一定の方向に押し出します。

主に以下のような使い方を想定しています。

- SvgMeshProviderで取り込んだパスデータを押し出す
- 「メッシュの中心を基準にする」した上で、「オフセットの設定」からScaleを指定することで、メッシュを膨らませる