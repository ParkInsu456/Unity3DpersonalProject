// 강동욱 튜터님 코드
// MeshCollider 자동으로 설정하는 기능도 코드도 첨부합니다!
// 주의사항: Editor 폴더 안에 넣을 것! [없으면 새로 만들어서 넣어주면 됨] 안그러면 빌드 시에 빌드 에러 뜹니다


using UnityEngine;
using UnityEditor;

public static class MeshColliderExtensions
{
    [MenuItem("CONTEXT/MeshRenderer/Adjust to MeshRenderer Bounds")]
    private static void AdjustBounds(MenuCommand command)
    {
        MeshRenderer meshRenderer = (MeshRenderer)command.context;
        BoxCollider boxCollider = meshRenderer.GetComponent<BoxCollider>();

        if (boxCollider == null)
            boxCollider = meshRenderer.gameObject.AddComponent<BoxCollider>();

        boxCollider.center = meshRenderer.bounds.center - meshRenderer.transform.position;
        boxCollider.size = meshRenderer.bounds.size;
    }
}