# Về hỗ trợ quốc tế hóa (Internationalization)

## Cách xử lý text hỗ trợ quốc tế hóa (LocalizeText)

### Thêm item
- Source file chỉnh sửa
    - `Assets/Libs/Localization/LocalizeTextData.cs`
```
    [Header("Sample Today:")]
    [SerializeField, TextArea]
    protected string today = string.Empty;
    public string Today => today;
```
- Hãy thêm [Header] để dễ kiểm tra trên Inspector
- Bố trí hạng mục string tại [SerializeField]
###  Asset thêm text
- `Assets/_HyperCasualTemplate/Data/Localization/Text/LocalizeTextData_XX.asset`
### Cách sử dụng text
-  Ví dụ cho trường hợp trình tự bị thay đổi do dịch thuật, như là date, v.v..
    - `Assets/_HyperCasualTemplate/Scripts/LocalizeSample_Today.cs`
```
var now = DateTime.Now;
if (LocalizeText.instance != null)
{
	textMesh.text = string.Format(LocalizeText.instance.currTextData.Today, now.Year, now.Month, now.Day);
}
```

## Cách sử dụng UI hỗ trợ quốc tế hóa (LocalizeUIData/TextM17N)

### Chuẩn bị LocalizeUIData asset
- Tại Project, click phải ⇒　Create > HyperCasualTemplate > Localization > LocalizeUIData
- Thêm danh sách ngôn ngữ cần thiết dựa trên mẫu `Assets/_HyperCasualTemplate/Data/Localization/UI/Sample/TouchToScreen.asset` 
### Sử dụng TextM17N component thay cho Text component
1. Create Empty tại nơi cần thiết để tạo GameObject
2. Thêm Text(M17N) bằng Add Component (Rect Transform v.v.sẽ được tự động thêm vào)
3. Thiếp lập LocalizeUIData asset bạn muốn assign, cho Localize Data
- Sample
    - SafeAreaPlate/Footer/Text trong `Assets/_HyperCasualTemplate/Prefabs/UI/UICanvas.prefab` の SafeAreaPlate/Footer/Text
### Thời điểm text được sử dụng
- Text và Font sẽ được chuyển đổi với ngôn ngữ hiện tại khi thực hiện `Start()`của TextM17N component
- Do không hỗ trợ `chuỗi chỉ định định dạng` nên nếu cần thì sử dụng cơ chế LocalizaText

## Phương pháp debug
- Có thể chuyển đổi ngôn ngữ hiện tại với Language ở DEBUG menu
- Để chuyển phần đang sử dụng TextM17N, bạn cần tải lại scene, v.v..
    - Ví dụ, sử dụng Restart của DEBUG menu.
