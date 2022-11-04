##########
# 高橋風雅さんのコード
# 大槻変更済
##########

# モジュールのインポート
import datetime
import tkinter
import tobii_research as tr
import time
import pandas as pd


# アイトラッカーに接続
found_eyetrackers = tr.find_all_eyetrackers()
my_eyetracker = found_eyetrackers[0]
print("Address: " + my_eyetracker.address)
print("Model: " + my_eyetracker.model)
print("Name (It's OK if this is empty): " + my_eyetracker.device_name)
print("Serial number: " + my_eyetracker.serial_number)

# データ入れるための変数
Gaze = []
GazeColumnsName = [
    "datetime",
    "system_timestamp",
    "right_gaze_point_on_display_area_x",
    "right_gaze_point_on_display_area_y",
    "left_gaze_point_on_display_area_x",
    "left_gaze_point_on_display_area_y",
    "right_gaze_point_validity",
    "left_gaze_point_validity",
    "right_pupill_diameter",
    "left_pupill_diameter",
    "right_pupill_validity",
    "left_pupill_validity",
]
UserEyePosition = (
    []
)  # list for data of User Eye Position as a normalized three valued tuple(x,y,z)
UserEyePositionColumnsName = [
    "datetime",
    "right_user_position_x",
    "right_user_position_y",
    "right_user_position_z",
    "left_user_position_x",
    "left_user_position_y",
    "left_user_position_z",
]

# 　保存ファイル名
fileNameSuggestion = "{0:%Y-%m-%d-%H-%M-%S}".format(datetime.datetime.now())

# GUIウィンドウ（フレーム）の作成
gui_root = tkinter.Tk()
# ウィンドウの名前を設定
gui_root.title("eye_tracker")
# ウィンドウの大きさを設定 横×縦
yoko = 240
tate = 100
gui_root.geometry("{}x{}".format(yoko, tate))


def gaze_data_callback1(gaze_data):
    # Print gaze points of left and right eye
    # Print gaze points of left and right eye
    global Gaze
    jikoku = time.time()
    sts = int(gaze_data["system_time_stamp"])
    x_r = gaze_data["right_gaze_point_on_display_area"][0]
    y_r = gaze_data["right_gaze_point_on_display_area"][1]
    x_l = gaze_data["left_gaze_point_on_display_area"][0]
    y_l = gaze_data["left_gaze_point_on_display_area"][1]
    p_r = gaze_data["right_pupil_diameter"]
    p_l = gaze_data["left_pupil_diameter"]
    val_p_r = gaze_data["right_pupil_validity"]
    val_p_l = gaze_data["left_pupil_validity"]
    val_g_r = gaze_data["right_gaze_point_validity"]
    val_g_l = gaze_data["left_gaze_point_validity"]

    Gaze.append(
        [jikoku, sts, x_r, x_l, y_r, y_l, val_g_r, val_g_l, p_r, p_l, val_p_r, val_p_l]
    )
    # print([jikoku,sts,x_r,x_l,y_r,y_l,val_g_r,val_g_l,p_r,p_l,val_p_r,val_p_l])


def gaze_data_callback2(gaze_data):
    global UserEyePosition
    jikoku = time.time()
    pos_r_x = gaze_data["right_user_position"][0]
    pos_r_y = gaze_data["right_user_position"][1]
    pos_r_z = gaze_data["right_user_position"][2]
    pos_l_x = gaze_data["left_user_position"][0]
    pos_l_y = gaze_data["left_user_position"][1]
    pos_l_z = gaze_data["left_user_position"][2]
    UserEyePosition.append(
        [jikoku, pos_r_x, pos_r_y, pos_r_z, pos_l_x, pos_l_y, pos_l_z]
    )
    print([pos_r_x])


def startbutton_callback():
    my_eyetracker.subscribe_to(
        tr.EYETRACKER_USER_POSITION_GUIDE, gaze_data_callback2, as_dictionary=True
    )
    my_eyetracker.subscribe_to(
        tr.EYETRACKER_GAZE_DATA, gaze_data_callback1, as_dictionary=True
    )


def stopbotton_callback():
    global Gaze
    global UserEyePosition
    global GazeColumnsName
    global UserEyePositionColumnsName
    global fileNameSuggestion

    my_eyetracker.unsubscribe_from(
        tr.EYETRACKER_USER_POSITION_GUIDE, gaze_data_callback2
    )
    my_eyetracker.unsubscribe_from(tr.EYETRACKER_GAZE_DATA, gaze_data_callback1)
    # print(S)
    # print(R)
    if Gaze != []:
        df1 = pd.DataFrame(Gaze, columns=GazeColumnsName)
        df2 = pd.DataFrame(UserEyePosition, columns=UserEyePositionColumnsName)
        df1.set_index("datetime")
        df2.set_index("datetime")
        fileNameSuggestion = txt.get()
        df1.to_excel("./" + fileNameSuggestion + "_GazeTest.xlsx")
        df2.to_excel("./" + fileNameSuggestion + "_UserPosition.xlsx")

    # 欠損分補完,時刻修正はしないように修正
    #  データ書き込み

    # 　各変数を初期化
    Gaze = []
    UserEyePosition = []
    fileNameSuggestion = "{0:%Y-%m-%d-%H-%M-%S}".format(datetime.datetime.now())
    txt.delete(0, tkinter.END)
    txt.insert(tkinter.END, fileNameSuggestion)


# フレームの定義
frame_up = tkinter.Frame(gui_root)
frame_down = tkinter.Frame(gui_root)
# ボタンの作成（text=ボタンに表示されるテキスト, command=ボタンを押したときに呼び出す関数 bg = 背景色, fg=文字色, width = 横幅）
# ボタンの配置　place()指定した座標pack(x=100,y=100)とか、pack()縦か横leftで左詰めtopで上から、grid()格子状詳細はググって
button1 = tkinter.Button(frame_up, text="スタート", command=startbutton_callback, width=15)
button2 = tkinter.Button(frame_up, text="ストップ", command=stopbotton_callback, width=15)

# ファイル名エントリ
lbl = tkinter.Label(frame_down, text="ファイル名")
txt = tkinter.Entry(frame_down, width=20)
txt.delete(0, tkinter.END)
txt.insert(tkinter.END, fileNameSuggestion)

frame_up.pack(side=tkinter.TOP, expand=True, fill=tkinter.BOTH)
frame_down.pack(side=tkinter.BOTTOM)

button1.pack(fill="both", side="left", padx=3)
button2.pack(fill="both", side="left", padx=3)
lbl.pack()
txt.pack()


# イベントループ（TK上のイベントを捕捉し、適切な処理を呼び出すイベントディスパッチャ）
gui_root.mainloop()
