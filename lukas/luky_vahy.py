import datetime as dt
import matplotlib.pyplot as plt
import numpy as np

def parse_line(line: str, year: int) -> tuple[dt.date, float]:
    # format "dd.mm-weight"
    line = line.lower().replace("ba", "").replace(" ", "").replace("✅", "")
    date, weight = line.split('-')
    day, month = date.split('.')
    weight = weight.replace(',', '.')
    return dt.date(year, int(month), int(day)), float(weight)

def parse_file(file_name: str) -> dict[dt.date, float]:
    weights = {}
    with open(file_name, "r") as f:
        curr_year = 2021
        prev_month = 12
        for line in f:
            date, weight = parse_line(line, curr_year)
            if prev_month == 12 and date.month == 1:
                curr_year += 1
                date = dt.date(curr_year, date.month, date.day)
            prev_month = date.month
            weights[date] = weight
    return weights

def fill_gaps(weights: np.ndarray) -> np.ndarray:
    # fill weights for missing dates linearly between the last known weight and the next known weight
    weights = weights.copy()
    gap_start_ix = None
    i = 0
    while i < weights.shape[0]: 
        if weights[i] == 0:
            gap_start_ix = i
            while i < weights.shape[0] and weights[i] == 0:
                i += 1
            gap_length = i - gap_start_ix
            gap_step = (weights[i] - weights[gap_start_ix - 1]) / gap_length
            for j in range(gap_start_ix, i):
                weights[j] = weights[j-1] + gap_step
            gap_start_ix = None
        i += 1
    return weights


def main():
    weights = parse_file("luky_vahy")

    start_date = min(weights.keys())
    days_total = (max(weights.keys()) - start_date).days + 1
    dates_axis = [start_date + dt.timedelta(days=i) for i in range(days_total)]

    weights_arr = np.zeros((days_total, 2))
    weights_arr[:, 0] = np.arange(days_total)

    # fill the weights to array from the dict
    for i, (date, weight) in enumerate(weights.items()):
        i = (date - start_date).days
        weights_arr[i, 1] = weight

    # rolling average
    weights_arr_rolling_avg = weights_arr.copy()
    weights_arr_rolling_avg[:, 1] = fill_gaps(weights_arr[:, 1])
    weights_arr_rolling_avg[:, 1] = np.convolve(weights_arr_rolling_avg[:, 1], np.ones((7,))/7, mode='same')

    # drop the first 7 days
    weights_arr_rolling_avg = weights_arr_rolling_avg[7:, :]
    # drop the last 7 days
    weights_arr_rolling_avg = weights_arr_rolling_avg[:-7, :]
    dates_axis_rolling_avg = dates_axis[7:-7]


    plt.plot(dates_axis, fill_gaps(weights_arr[:,1]))
    plt.plot(dates_axis_rolling_avg, weights_arr_rolling_avg[:,1])
    plt.xlabel("datum")
    plt.ylabel("vaha")
    plt.title("Luky sigma male body weight progress")
    plt.legend(["vaha", "rolling average (7d)"])
    plt.grid()
    plt.show()


if __name__ == "__main__":
    main()

        
