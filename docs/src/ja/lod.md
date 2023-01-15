# LOD

## MeshWeaverにおけるLOD

MeshWeaverにおける**LOD（Level of Detail）**とは、ポリゴン数を削減したモデルを生成する仕組みです。

MeshWeaverの各プロバイダーコンポーネントには**LOD Mask（LOD マスク）**を設定することができます。

PathProviderとMeshProviderは、LODレベルがLODマスクに含まれている場合のみ、メッシュやパスを出力します。
LODレベルがLODマスクから外れている場合、何も出力しません。

ModifierProviderは、LODレベルがLODマスクに含まれている場合のみ、モディファイアによるデータの加工が適用されます。
LODレベルがLODマスクから外れている場合、元のデータを出力します。

MeshWeaverには固定で４つのLODが存在します。

- LOD 0 標準的な状態です。
- LOD 1 LOD 0と比較してポリゴン数が省略された状態です。
- LOD 2 
- Collider メッシュコライダー用の簡略化されたメッシュを定義するレベルです。

初期設定では、MeshWeaverの出力するプレハブには、Collider設定で出力されたメッシュがメッシュコライダーとして設定されています。

### まとめると

レンダラー用のメッシュとコライダー用のメッシュをいっぺんにモデリングするための機能です！
