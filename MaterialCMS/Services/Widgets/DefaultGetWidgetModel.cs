﻿using MaterialCMS.Entities.Widget;

namespace MaterialCMS.Services.Widgets
{
    public sealed class DefaultGetWidgetModel : GetWidgetModelBase
    {
        private DefaultGetWidgetModel()
        {

        }

        static DefaultGetWidgetModel()
        {
            Instance = new DefaultGetWidgetModel();
        }

        public readonly static DefaultGetWidgetModel Instance;
        public override object GetModel(Widget widget)
        {
            return widget;
        }
    }
}