﻿using CatRssReader.Common;
using CatRssReader.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// セクション ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234229 を参照してください

namespace CatRssReader
{
    /// <summary>
    /// 単一のグループ内のアイテムのプレビューを含め、グループの概要を表示する
    /// ページです。
    /// </summary>
    public sealed partial class SectionPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// NavigationHelper は、ナビゲーションおよびプロセス継続時間管理を
        /// 支援するために、各ページで使用します。
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// これは厳密に型指定されたビュー モデルに変更できます。
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }


        public SectionPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        /// <summary>
        /// このページには、移動中に渡されるコンテンツを設定します。前のセッションからページを
        /// 再作成する場合は、保存状態も指定されます。
        /// </summary>
        /// <param name="sender">
        /// イベントのソース (通常、<see cref="NavigationHelper"/>)
        /// </param>
        /// <param name="e">このページが最初に要求されたときに
        /// <see cref="Frame.Navigate(Type, Object)"/> に渡されたナビゲーション パラメーターと、
        /// 前のセッションでこのページによって保存された状態の辞書を提供する
        /// イベント データ。ページに初めてアクセスするとき、状態は null になります。</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: 問題のドメインでサンプル データを置き換えるのに適したデータ モデルを作成します
            var group = await SampleDataSource.GetGroupAsync((String)e.NavigationParameter);
            this.DefaultViewModel["Group"] = group;
            this.DefaultViewModel["Items"] = group.Items;
        }

        /// <summary>
        /// アイテムがクリックされたときに呼び出されます。
        /// </summary>
        /// <param name="sender">クリックされたアイテムを表示する GridView。</param>
        /// <param name="e">クリックされたアイテムを説明するイベント データ。</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 適切な移動先のページに移動し、新しいページを構成します。
            // このとき、必要な情報をナビゲーション パラメーターとして渡します
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(ItemPage), itemId);
        }

        #region NavigationHelper の登録

        /// このセクションに示したメソッドは、NavigationHelper がページの
        /// ナビゲーション メソッドに応答できるようにするためにのみ使用します。
        /// 
        /// ページ固有のロジックは、
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// および <see cref="GridCS.Common.NavigationHelper.SaveState"/> のイベント ハンドラーに配置する必要があります。
        /// LoadState メソッドでは、前のセッションで保存されたページの状態に加え、
        /// ナビゲーション パラメーターを使用できます。

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}