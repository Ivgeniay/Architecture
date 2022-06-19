using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review
{
    public class ObserverLogger : IObserver
    {
        public ObserverLogger()
        {
            reviews = new List<IReview>();
        }

        public ObserverLogger(IReview review)
        {
            this.reviews = new List<IReview>();
            this.reviews.Add(review);
            review.OnChange += OnChange;
        }
        public ObserverLogger(IReview[] reviews)
        {
            this.reviews = new List<IReview>(reviews);
            foreach (IReview e in reviews)
                e.OnChange += OnChange;
        }

        public void AddReview(IReview review)
        {
            if (reviews.Contains(review))
                return;
            this.reviews.Add(review);
            review.OnChange += OnChange;
        }

        public void Dispose()
        {
            foreach (IReview e in reviews)
                e.OnChange -= OnChange;
        }

        private void OnChange(object obj)
        {
            Debug.Log($"Type \"{obj.GetType().Name}\" value change => {obj.ToString()} ");
        }

        private List<IReview> reviews;

    }
}
